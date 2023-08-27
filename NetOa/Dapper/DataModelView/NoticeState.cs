using NetOa.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModelView
{
    public class NoticeState:Notice
    {

        public string statusMsg { set; get; }
        public string Remarks { set; get; }

    }
}
