using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SL.Composite;
using SL.DAL.Contracts;
using SL.DAL.Tools;

namespace SL.DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server encargado de almacenar y gestionar
    /// Patentes, Familias y sus relaciones jerárquicas.
    /// Implementa las operaciones definidas en <see cref="IPermissionRepository"/>.
    /// </summary>
    internal class PermissionRepository : IPermissionRepository
    {
        #region Statements

        /// <summary>
        /// Sentencia SQL para insertar un componente de permiso (Patente o Familia).
        /// </summary>
        private string InsertStatementPermissionComponent =>
            @"INSERT INTO [dbo].[PermissionComponent] 
              (IdComponent, Name, ComponentType, FormName, DVH)
              VALUES (@IdComponent, @Name, @ComponentType, @FormName, @DVH)";

        /// <summary>
        /// Sentencia SQL para registrar una relación jerárquica entre componentes.
        /// </summary>
        private string InsertStatementComponentRelationship =>
            @"INSERT INTO [dbo].[ComponentRelationship] (ParentId, ChildId)
              VALUES (@ParentId, @ChildId)";

        /// <summary>
        /// Sentencia SQL para asignar familias a un usuario.
        /// </summary>
        private string InsertStatementUserFamily =>
            @"INSERT INTO [dbo].[UserPermissionComponent] (UserId, IdComponent)
              VALUES (@UserId, @IdComponent)";

        /// <summary>
        /// Sentencia SQL para actualizar un componente (Patente/Familia).
        /// </summary>
        private string UpdateStatementPermissionComponent =>
            @"UPDATE PermissionComponent
              SET Name = @Name, FormName = @FormName, DVH = @DVH
              WHERE IdComponent = @IdComponent";

        /// <summary>
        /// Sentencia SQL para eliminar todas las relaciones del componente padre.
        /// </summary>
        private const string DeleteStatementComponentChildren =
            @"DELETE FROM ComponentRelationship WHERE ParentId = @ParentId";

        /// <summary>
        /// Sentencia SQL específica para actualizar datos de una familia.
        /// </summary>
        private const string UpdateStatementFamily =
            @"UPDATE PermissionComponent
              SET Name = @Name, DVH = @DVH
              WHERE IdComponent = @IdComponent";

        /// <summary>
        /// Sentencia SQL para eliminar las familias asignadas a un usuario.
        /// </summary>
        private string DeleteStatementUserFamilies =>
            @"DELETE FROM [dbo].[UserPermissionComponent]
              WHERE UserId = @UserId
              AND IdComponent IN (
                    SELECT IdComponent FROM PermissionComponent 
                    WHERE ComponentType = 'Familia'
              )";

        /// <summary>
        /// Sentencia SQL para obtener todas las familias registradas.
        /// </summary>
        private string SelectAllStatementFamilies =>
            @"SELECT IdComponent, Name
              FROM [dbo].[PermissionComponent]
              WHERE ComponentType = 'Familia'";

        /// <summary>
        /// Sentencia SQL para obtener todos los componentes y sus datos.
        /// </summary>
        private string SelectStatementAllComponents =>
            @"SELECT IdComponent, Name, ComponentType, FormName, DVH
              FROM [dbo].[PermissionComponent]";

        /// <summary>
        /// Consulta recursiva CTE para obtener todas las patentes asignadas a un usuario,
        /// incluyendo las heredadas por familias.
        /// </summary>
        private string SelectStatementPermissionsForUser =>
            @"WITH RecursivePermissions AS (
                SELECT pc.IdComponent, pc.Name, pc.ComponentType, pc.FormName
                FROM PermissionComponent pc
                INNER JOIN UserPermissionComponent upc 
                        ON pc.IdComponent = upc.IdComponent
                WHERE upc.UserId = @UserId

                UNION ALL

                SELECT pc2.IdComponent, pc2.Name, pc2.ComponentType, pc2.FormName
                FROM PermissionComponent pc2
                INNER JOIN ComponentRelationship cr 
                        ON cr.ChildId = pc2.IdComponent
                INNER JOIN RecursivePermissions rp 
                        ON rp.IdComponent = cr.ParentId
            )
            SELECT DISTINCT IdComponent, Name, ComponentType, FormName
            FROM RecursivePermissions
            WHERE ComponentType = 'Patente'";

        #endregion

        /// <summary>
        /// Asigna una lista de familias a un usuario determinado.
        /// </summary>
        public void AssignFamiliesToUser(Guid userId, List<Guid> familyIds)
        {
            foreach (var familyId in familyIds)
            {
                SqlHelper.ExecuteNonQuery(
                    InsertStatementUserFamily,
                    CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@IdComponent", familyId)
                    });
            }
        }

        /// <summary>
        /// Obtiene todas las familias registradas en la tabla PermissionComponent.
        /// </summary>
        public List<Familia> GetAllFamilies()
        {
            var result = new List<Familia>();

            using (var reader = SqlHelper.ExecuteReader(SelectAllStatementFamilies, CommandType.Text))
            {
                while (reader.Read())
                {
                    var familia = new Familia(reader.GetString(1), null)
                    {
                        IdComponent = reader.GetGuid(0)
                    };
                    result.Add(familia);
                }
            }

            return result;
        }

        /// <summary>
        /// Obtiene todos los componentes existentes (Patentes y Familias).
        /// Incluye Name, FormName, Tipo y DVH.
        /// </summary>
        public List<PermissionComponent> GetAllPermissionComponents()
        {
            var result = new List<PermissionComponent>();

            using (var reader = SqlHelper.ExecuteReader(SelectStatementAllComponents, CommandType.Text))
            {
                while (reader.Read())
                {
                    var id = reader.GetGuid(0);
                    var name = reader.GetString(1);
                    var type = reader.GetString(2);
                    var formName = reader.IsDBNull(3) ? null : reader.GetString(3);
                    var dvh = reader.IsDBNull(4) ? null : reader.GetString(4);

                    if (type == "Patente")
                    {
                        result.Add(new Patente
                        {
                            IdComponent = id,
                            Name = name,
                            FormName = formName,
                            DVH = dvh
                        });
                    }
                    else if (type == "Familia")
                    {
                        result.Add(new Familia(name, null)
                        {
                            IdComponent = id,
                            DVH = dvh
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Obtiene todas las relaciones padre → hijo registradas en el sistema.
        /// </summary>
        public List<(Guid ParentId, Guid ChildId)> GetAllPermissionRelations()
        {
            var relaciones = new List<(Guid ParentId, Guid ChildId)>();

            using (var reader = SqlHelper.ExecuteReader(
                       "SELECT ParentId, ChildId FROM ComponentRelationship",
                       CommandType.Text))
            {
                while (reader.Read())
                {
                    relaciones.Add((reader.GetGuid(0), reader.GetGuid(1)));
                }
            }

            return relaciones;
        }

        /// <summary>
        /// Obtiene todas las patentes de un usuario, incluyendo las heredadas
        /// desde familias mediante CTE recursiva.
        /// </summary>
        public List<PermissionComponent> GetPermissionsForUser(Guid userId)
        {
            var result = new List<PermissionComponent>();

            using (var reader = SqlHelper.ExecuteReader(
                       SelectStatementPermissionsForUser,
                       CommandType.Text,
                       new[] { new SqlParameter("@UserId", userId) }))
            {
                while (reader.Read())
                {
                    var id = reader.GetGuid(0);
                    var name = reader.GetString(1);
                    var type = reader.GetString(2);
                    var formName = reader.IsDBNull(3) ? null : reader.GetString(3);

                    if (type.Equals("Patente", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(new Patente
                        {
                            IdComponent = id,
                            Name = name,
                            FormName = formName,
                            DVH = null
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Elimina todas las familias actualmente asignadas a un usuario.
        /// </summary>
        public void RemoveFamiliesFromUser(Guid userId)
        {
            SqlHelper.ExecuteNonQuery(
                DeleteStatementUserFamilies,
                CommandType.Text,
                new[] { new SqlParameter("@UserId", userId) });
        }

        /// <summary>
        /// Inserta una nueva familia junto con sus relaciones con componentes hijos.
        /// Incluye el DVH correspondiente.
        /// </summary>
        public void SaveFamily(Familia family)
        {
            family.IdComponent = family.IdComponent == Guid.Empty
                ? Guid.NewGuid()
                : family.IdComponent;

            SqlHelper.ExecuteNonQuery(
                InsertStatementPermissionComponent,
                CommandType.Text,
                new[]
                {
                    new SqlParameter("@IdComponent", family.IdComponent),
                    new SqlParameter("@Name", family.Name),
                    new SqlParameter("@ComponentType", "Familia"),
                    new SqlParameter("@FormName", DBNull.Value),
                    new SqlParameter("@DVH", (object)family.DVH ?? DBNull.Value)
                });

            foreach (var child in family.GetChildren())
            {
                SqlHelper.ExecuteNonQuery(
                    InsertStatementComponentRelationship,
                    CommandType.Text,
                    new[]
                    {
                        new SqlParameter("@ParentId", family.IdComponent),
                        new SqlParameter("@ChildId", child.IdComponent)
                    });
            }
        }

        /// <summary>
        /// Actualiza una familia (Name, DVH) y reemplaza completamente
        /// sus relaciones padre → hijos en la tabla ComponentRelationship.
        /// </summary>
        public void UpdateFamily(Familia familia)
        {
            SqlHelper.ExecuteNonQuery(
                UpdateStatementFamily,
                CommandType.Text,
                new[]
                {
                    new SqlParameter("@IdComponent", familia.IdComponent),
                    new SqlParameter("@Name", familia.Name),
                    new SqlParameter("@DVH", (object)familia.DVH ?? DBNull.Value)
                });

            SqlHelper.ExecuteNonQuery(
                DeleteStatementComponentChildren,
                CommandType.Text,
                new[] { new SqlParameter("@ParentId", familia.IdComponent) });

            foreach (var child in familia.GetChildren())
            {
                SqlHelper.ExecuteNonQuery(
                    InsertStatementComponentRelationship,
                    CommandType.Text,
                    new[]
                    {
                        new SqlParameter("@ParentId", familia.IdComponent),
                        new SqlParameter("@ChildId", child.IdComponent)
                    });
            }
        }

        /// <summary>
        /// Inserta una patente en PermissionComponent, incluyendo DVH y FormName.
        /// </summary>
        public void SavePatent(Patente patente)
        {
            patente.IdComponent = patente.IdComponent == Guid.Empty
                ? Guid.NewGuid()
                : patente.IdComponent;

            SqlHelper.ExecuteNonQuery(
                InsertStatementPermissionComponent,
                CommandType.Text,
                new[]
                {
                    new SqlParameter("@IdComponent", patente.IdComponent),
                    new SqlParameter("@Name", patente.Name),
                    new SqlParameter("@ComponentType", "Patente"),
                    new SqlParameter("@FormName", patente.FormName != null ? (object)patente.FormName : DBNull.Value),
                    new SqlParameter("@DVH", patente.DVH != null ? (object)patente.DVH : DBNull.Value)
                });
        }

        /// <summary>
        /// Actualiza los datos de una patente (Name, FormName, DVH).
        /// </summary>
        public void UpdatePatent(Patente patente)
        {
            SqlHelper.ExecuteNonQuery(
                UpdateStatementPermissionComponent,
                CommandType.Text,
                new[]
                {
                    new SqlParameter("@IdComponent", patente.IdComponent),
                    new SqlParameter("@Name", patente.Name),
                    new SqlParameter("@FormName", patente.FormName != null ? (object)patente.FormName : DBNull.Value),
                    new SqlParameter("@DVH", patente.DVH != null ? (object)patente.DVH : DBNull.Value)
                });
        }
    }
}