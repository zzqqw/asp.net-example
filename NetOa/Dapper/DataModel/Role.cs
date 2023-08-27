using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOA.Dapper.DataModel
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    public class Role
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        { get; set; }
    }
}
