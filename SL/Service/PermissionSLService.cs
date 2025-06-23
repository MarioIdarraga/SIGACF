using System;
using System.Collections.Generic;
using SL.Composite;
using SL.DAL.Contracts;
using SL.Factory;
using SL.Helpers;

namespace SL.Service
{
    public class PermissionSLService
    {
        private readonly IPermissionRepository _repo;

        public PermissionSLService()
        {
            _repo = SLFactory.Current.GetPermissionRepository();
        }

        public List<Patente> GetPatentesByUser(Guid userId)
        {
            var permisos = _repo.GetPermissionsForUser(userId);
            return PermissionHelper.FlattenPermissions(permisos);
        }
    }
}



