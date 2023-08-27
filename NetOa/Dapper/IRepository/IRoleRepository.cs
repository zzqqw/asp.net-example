using NetOA.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOA.Dapper.IRepository
{
    /// <summary>
    /// 角色的仓储接口
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// 本询角色
        /// </summary>
        /// <returns></returns>
        List<Role> GetRoles();     

    }
}
