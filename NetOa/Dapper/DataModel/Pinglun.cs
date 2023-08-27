using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModel
{
    public class Pinglun
    {
        public int ID { set; get; }

        public int UserID { set; get; }
        public int ArticlesID { set; get; }
        public string content { set; get; }
        public DateTime CreateTime { set; get; }

    }
}
