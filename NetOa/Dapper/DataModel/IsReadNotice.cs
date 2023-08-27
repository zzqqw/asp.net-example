using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModel
{
    public class IsReadNotice
    {
        public int ID { set; get; }
        public int UserID { set; get; }
        public string NoticeID { set; get; }
        public string statusMsg { set; get; }
        public string  Remarks { set; get; }
        public DateTime CreateTime { get;  set; }
    }
}
