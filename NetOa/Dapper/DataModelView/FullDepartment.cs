using NetOA.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModelView
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class FullDepartment : Department
    {
        /// <summary>
        /// 上级部门名称
        /// </summary>      
        public string PDepartmentName { get; set; }
    }
}
