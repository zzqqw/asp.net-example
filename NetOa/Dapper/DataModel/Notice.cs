using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModel
{
    public class Notice
    {

        public int ID { set; get; }
        public string NoticeType { set; get; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string NoticeTitle { set; get; }
        /// <summary>
        /// 通知内容
        /// </summary>
        public string NoticeContent { set; get; }
        /// <summary>
        /// 发布人
        /// </summary>
        public int FormUserID { set; get; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string ToUserID { set; get; }

        public DateTime CreateTime { set; get; }

    }
}
