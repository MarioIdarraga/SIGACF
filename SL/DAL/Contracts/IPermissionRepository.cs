using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Composite;

namespace SL.DAL.Contracts
{
    public interface IPermissionRepository
    {
        List<PermissionComponent> GetPermissionsForUser(Guid userId);
    }
}
