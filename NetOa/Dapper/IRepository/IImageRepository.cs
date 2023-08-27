using NetOa.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.IRepository
{
    public interface IImageRepository
    {
        bool AddImage(Image image);
    }
}
