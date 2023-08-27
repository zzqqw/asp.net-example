using NetOA.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModelView
{
    
    /// <summary>
    /// 用户色角实体类
    /// </summary>
    public class UserRole : User
    {
        /// <summary>
        /// 用户角色
        /// </summary>        
        public string RoleName
        { get; set; }
    }
}
