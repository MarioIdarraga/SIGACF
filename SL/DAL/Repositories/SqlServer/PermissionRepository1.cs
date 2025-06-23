using System;
using System.Collections.Generic;
using SL.Composite;
using SL.DAL.Contracts;

namespace SL.DAL.Repositories.SqlServer
{
    internal class PermissionRepository : IPermissionRepository
    {
        public List<PermissionComponent> GetPermissionsForUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}