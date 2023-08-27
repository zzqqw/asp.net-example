using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NetOA.Dapper.DataModel;
using NetOA.Dapper.IRepository;

namespace NetOA.Dapper.Repository
{
    /// <summary>
    /// 角色的仓储类
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        INetOADB _workingDB;
        public RoleRepository(INetOADB workingDB)
        {
            _workingDB = workingDB;

        }
        /// <summary>
        /// 本询角色
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            return _workingDB.Query<Role>("select * from roles").ToList();
        }
    }
}
