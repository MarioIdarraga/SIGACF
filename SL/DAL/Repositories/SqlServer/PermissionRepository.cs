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
    internal class PermissionRepository : IPermissionRepository
    {

        #region Statements

        private string InsertStatementPermissionComponent => @"INSERT INTO [dbo].[PermissionComponent] (IdComponent, Name, ComponentType, FormName, DVH)
      VALUES (@IdComponent, @Name, @ComponentType, @FormName, @DVH)";

        private string InsertStatementComponentRelationship => @"INSERT INTO [dbo].[ComponentRelationship] (ParentId, ChildId) VALUES (@ParentId, @ChildId)";

        private string InsertStatementUserFamily => @"INSERT INTO [dbo].[UserPermissionComponent] (UserId, IdComponent) VALUES (@UserId, @IdComponent)";

        private string UpdateStatementPermissionComponent => @"UPDATE PermissionComponent SET Name = @Name, FormName = @FormName, DVH = @DVH WHERE IdComponent = @IdComponent";

        private const string DeleteStatementComponentChildren = @"DELETE FROM ComponentRelationship WHERE ParentId = @ParentId";

        private const string UpdateStatementFamily = @"UPDATE PermissionComponent SET Name = @Name, DVH = @DVH WHERE IdComponent = @IdComponent";
        private string DeleteStatementUserFamilies => @"DELETE FROM [dbo].[UserPermissionComponent] WHERE UserId = @UserId AND IdComponent IN (
          SELECT IdComponent FROM PermissionComponent WHERE ComponentType = 'Familia'
      )";

        private string SelectAllStatementFamilies =>
            @"SELECT IdComponent, Name FROM [dbo].[PermissionComponent] WHERE ComponentType = 'Familia'";

        private string SelectStatementAllComponents => @"SELECT IdComponent, Name, ComponentType, FormName, DVH FROM [dbo].[PermissionComponent]";

        private string SelectStatementPermissionsForUser =>
            @"WITH RecursivePermissions AS (
                    SELECT pc.IdComponent, pc.Name, pc.ComponentType, pc.FormName
                    FROM PermissionComponent pc
                    INNER JOIN UserPermissionComponent upc ON pc.IdComponent = upc.IdComponent
                    WHERE upc.UserId = @UserId

                    UNION ALL

                    SELECT pc2.IdComponent, pc2.Name, pc2.ComponentType, pc2.FormName
                    FROM PermissionComponent pc2
                    INNER JOIN ComponentRelationship cr ON cr.ChildId = pc2.IdComponent
                    INNER JOIN RecursivePermissions rp ON rp.IdComponent = cr.ParentId
                    )
                    SELECT DISTINCT IdComponent, Name, ComponentType, FormName
                    FROM RecursivePermissions
                    WHERE ComponentType = 'Patente'";

        #endregion



        public void AssignFamiliesToUser(Guid userId, List<Guid> familyIds)
        {
            foreach (var familyId in familyIds)
            {
                SqlHelper.ExecuteNonQuery(InsertStatementUserFamily, System.Data.CommandType.Text,
                    new SqlParameter[]
                    {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@IdComponent", familyId)
                    });
            }
        }

        public List<Familia> GetAllFamilies()
        {
            var result = new List<Familia>();

            using (var reader = SqlHelper.ExecuteReader(SelectAllStatementFamilies, System.Data.CommandType.Text))
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

        public List<PermissionComponent> GetAllPermissionComponents()
        {
            var result = new List<PermissionComponent>();

            using (var reader = SqlHelper.ExecuteReader(SelectStatementAllComponents, System.Data.CommandType.Text))
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
                        var patente = new Patente
                        {
                            IdComponent = id,
                            Name = name,
                            FormName = formName,
                            DVH = dvh
                        };
                        result.Add(patente);
                    }
                    else if (type == "Familia")
                    {
                        var familia = new Familia(name, null)
                        {
                            IdComponent = id,
                            DVH = dvh
                        };
                        result.Add(familia);
                    }
                }
            }

            return result;
        }

        public List<(Guid ParentId, Guid ChildId)> GetAllPermissionRelations()
        {
            var relaciones = new List<(Guid ParentId, Guid ChildId)>();

            using (var reader = SqlHelper.ExecuteReader(
                   "SELECT ParentId, ChildId FROM ComponentRelationship", CommandType.Text))
            {
                while (reader.Read())
                {
                    var parentId = reader.GetGuid(0);
                    var childId = reader.GetGuid(1);
                    relaciones.Add((parentId, childId));   // ← alias ParentId / ChildId
                }
            }
            return relaciones;
        }

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

                    if (string.Equals(type, "Patente", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(new Patente
                        {
                            IdComponent = id,
                            Name = name,
                            FormName = formName,
                            DVH = null // no viene en este SELECT
                        });
                    }
                }
            }

            return result;
        }


        public void RemoveFamiliesFromUser(Guid userId)
        {
            SqlHelper.ExecuteNonQuery(DeleteStatementUserFamilies, System.Data.CommandType.Text,
        new SqlParameter[] { new SqlParameter("@UserId", userId) });
        }

        public void SaveFamily(Familia family)
        {

            family.IdComponent = family.IdComponent == Guid.Empty ? Guid.NewGuid() : family.IdComponent;


            SqlHelper.ExecuteNonQuery(InsertStatementPermissionComponent, System.Data.CommandType.Text,
                new SqlParameter[]
                {
            new SqlParameter("@IdComponent", family.IdComponent),
            new SqlParameter("@Name", family.Name),
            new SqlParameter("@ComponentType", "Familia"),
            new SqlParameter("@FormName", DBNull.Value),
            new SqlParameter("@DVH", (object)family.DVH ?? DBNull.Value)
                });

            // 2. Insertar relaciones con hijos
            foreach (var child in family.GetChildren())
            {
                SqlHelper.ExecuteNonQuery(InsertStatementComponentRelationship, System.Data.CommandType.Text,
                    new SqlParameter[]
                    {
                new SqlParameter("@ParentId", family.IdComponent),
                new SqlParameter("@ChildId", child.IdComponent)
                    });
            }
        }

        public void UpdateFamily(Familia familia)
        {
            // 1. Actualizar datos propios (Name y DVH)
            SqlHelper.ExecuteNonQuery(UpdateStatementFamily, CommandType.Text,
                new[]
                {
            new SqlParameter("@IdComponent", familia.IdComponent),
            new SqlParameter("@Name",         familia.Name),
            new SqlParameter("@DVH",          (object)familia.DVH ?? DBNull.Value)
                });

            // 2. Borrar relaciones existentes
            SqlHelper.ExecuteNonQuery(DeleteStatementComponentChildren, CommandType.Text,
                new[] { new SqlParameter("@ParentId", familia.IdComponent) });

            // 3. Insertar nuevas relaciones
            foreach (var child in familia.GetChildren())
            {
                SqlHelper.ExecuteNonQuery(InsertStatementComponentRelationship, CommandType.Text,
                    new[]
                    {
                new SqlParameter("@ParentId", familia.IdComponent),
                new SqlParameter("@ChildId",  child.IdComponent)
                    });
            }
        }

        public void SavePatent(Patente patente)
        {
            patente.IdComponent = patente.IdComponent == Guid.Empty ? Guid.NewGuid() : patente.IdComponent;

            SqlHelper.ExecuteNonQuery(InsertStatementPermissionComponent, System.Data.CommandType.Text,
                new SqlParameter[]
                {
            new SqlParameter("@IdComponent", patente.IdComponent),
            new SqlParameter("@Name", patente.Name),
            new SqlParameter("@ComponentType", "Patente"),
            new SqlParameter("@FormName", patente.FormName != null ? (object)patente.FormName : DBNull.Value),
            new SqlParameter("@DVH", patente.DVH != null ? (object)patente.DVH : DBNull.Value)
                });
        }



        public void UpdatePatent(Patente patente)
        {
            SqlHelper.ExecuteNonQuery(UpdateStatementPermissionComponent, CommandType.Text,
                new SqlParameter[]
                {
            new SqlParameter("@IdComponent", patente.IdComponent),
            new SqlParameter("@Name", patente.Name),
            new SqlParameter("@FormName", patente.FormName != null ? (object)patente.FormName : DBNull.Value),
            new SqlParameter("@DVH", patente.DVH != null ? (object)patente.DVH : DBNull.Value)
                });
        }
    }
}