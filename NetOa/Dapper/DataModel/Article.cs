﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModel
{
    public class Article
    {
        public int ID
        { get; set; }
        public DateTime CreateTime
        { get; set; }
        public int CreateUserID
        { get; set; }

        public string Title
        { get; set; }
        public string Content
        { get; set; }
        public int ColumnID { get; set; }


        public int  Type { get; set; }

        public int Click{ get; set; }
    }
}
