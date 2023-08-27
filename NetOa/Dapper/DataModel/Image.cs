using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.DataModel
{
    public class Image
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int Size { get; set; }
        public string fileName { get; set; }
        public string suffix { get; set; }
        public string filePath { get; set; }
        public string fileFullName { get; set; }
    }
}
