using System;
using System.Collections.Generic;
using SL.Composite;
using SL.DAL.Contracts;

namespace SL.DAL.Repositories.File
{
    internal class PermissionRepository : IPermissionRepository
    {
        public void AssignFamiliesToUser(Guid userId, List<Guid> familyIds)
        {
            throw new NotImplementedException();
        }

        public List<Familia> GetAllFamilies()
        {
            throw new NotImplementedException();
        }

        public List<PermissionComponent> GetAllPermissionComponents()
        {
            throw new NotImplementedException();
        }

        public List<PermissionComponent> GetPermissionsForUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFamiliesFromUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void SaveFamily(Familia family)
        {
            throw new NotImplementedException();
        }

        public void SavePatent(Patente patente)
        {
            throw new NotImplementedException();
        }

        public void UpdatePatent(Patente patente)
        {
            throw new NotImplementedException();
        }
    }
}