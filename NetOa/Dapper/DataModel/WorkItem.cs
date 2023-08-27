using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOA.Dapper.DataModel
{
    public class WorkItem
    {
        
        /// <summary>
        /// ID
        /// </summary>
        public int ID{ get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public String Area { get; set; }
        /// <summary>
        /// 相关人员
        /// </summary>
        public String RelevantPeople { get; set; }
        /// <summary>
        /// 归属单位
        /// </summary>
        public String Agency { get; set; }
        
        /// <summary>
        /// 作业内容
        /// </summary>
        public string WorkContent { get; set; }

        /// <summary>
        /// 作业时间日期
        /// </summary>
        public String RecordDate { get; set; }

        /// <summary>
        /// 经度：普通话拼音：jīng dù ； 英文：longitude 
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度：普通话拼音：wěi dù ； 英文：latitude ;
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime{ get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserID{ get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int Status{ get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public int ReviewerUserID { get; set; }

    }
}
