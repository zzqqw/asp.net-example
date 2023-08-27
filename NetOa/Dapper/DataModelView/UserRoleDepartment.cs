using NetOA.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModelView
{
    public class UserRoleDepartment: User
    {
        /// <summary>
        /// 用户角色
        /// </summary>        
        public int uid
        { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>        
        public string RoleName
        { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartMentName
        { get; set; }
    }
}
