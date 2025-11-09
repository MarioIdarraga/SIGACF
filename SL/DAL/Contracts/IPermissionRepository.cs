using System;
using System.Collections.Generic;
using SL.Composite;


namespace SL.DAL.Contracts
{
    public interface IPermissionRepository
    {
        List<PermissionComponent> GetPermissionsForUser(Guid userId);

        List<Familia> GetAllFamilies();

        void AssignFamiliesToUser(Guid userId, List<Guid> familyIds);

        void RemoveFamiliesFromUser(Guid userId);

        void SaveFamily(Familia family);

        void UpdateFamily(Familia familia);

        void SavePatent(Patente patente);

        void UpdatePatent(Patente patente);

        List<(Guid ParentId, Guid ChildId)> GetAllPermissionRelations();

        //Para implementar DVV
        List<PermissionComponent> GetAllPermissionComponents();


    }
}

