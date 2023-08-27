using NetOa.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModelView
{
    public class ArticleUserColumn:Article
    {
        public int aid { set; get; }
        public string ColumnName { set; get; }
        public string username { set; get; }
    }
}
