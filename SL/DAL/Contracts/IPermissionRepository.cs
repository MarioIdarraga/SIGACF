using System;
using System.Collections.Generic;
using SL.Composite;


namespace SL.DAL.Contracts
{
    /// <summary>
    /// Define las operaciones necesarias para la gestión de permisos,
    /// incluyendo familias, patentes y relaciones jerárquicas.
    /// Implementado por la capa DAL.
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// Obtiene todos los permisos asignados a un usuario,
        /// incluyendo patentes individuales y familias asociadas.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>Lista de componentes de permisos.</returns>
        List<PermissionComponent> GetPermissionsForUser(Guid userId);

        /// <summary>
        /// Obtiene todas las familias de permisos existentes en el sistema.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Familia"/>.</returns>
        List<Familia> GetAllFamilies();

        /// <summary>
        /// Asocia un conjunto de familias a un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="familyIds">Lista de identificadores de familias a asignar.</param>
        void AssignFamiliesToUser(Guid userId, List<Guid> familyIds);

        /// <summary>
        /// Elimina todas las familias asociadas a un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        void RemoveFamiliesFromUser(Guid userId);

        /// <summary>
        /// Guarda una nueva familia de permisos en el repositorio.
        /// </summary>
        /// <param name="family">Familia a guardar.</param>
        void SaveFamily(Familia family);

        /// <summary>
        /// Actualiza una familia de permisos existente.
        /// </summary>
        /// <param name="familia">Familia a actualizar.</param>
        void UpdateFamily(Familia familia);

        /// <summary>
        /// Guarda una nueva patente en el repositorio.
        /// </summary>
        /// <param name="patente">Patente a guardar.</param>
        void SavePatent(Patente patente);

        /// <summary>
        /// Actualiza una patente existente.
        /// </summary>
        /// <param name="patente">Patente a actualizar.</param>
        void UpdatePatent(Patente patente);

        /// <summary>
        /// Obtiene todas las relaciones jerárquicas entre familias y componentes,
        /// representadas como tuplas de ParentId y ChildId.
        /// </summary>
        /// <returns>Lista de relaciones de permisos.</returns>
        List<(Guid ParentId, Guid ChildId)> GetAllPermissionRelations();

        /// <summary>
        /// Obtiene todos los componentes de permisos (familias y patentes),
        /// utilizado para cálculo de DVV.
        /// </summary>
        /// <returns>Lista de componentes de permisos.</returns>
        List<PermissionComponent> GetAllPermissionComponents();
    }
}

