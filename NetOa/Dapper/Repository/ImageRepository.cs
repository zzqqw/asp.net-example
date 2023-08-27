using NetOa.Dapper.DataModel;
using NetOa.Dapper.IRepository;
using NetOA.Dapper.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.Dapper.Repository
{
    public class ImageRepository:IImageRepository
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        INetOADB _workingDB;

        public ImageRepository(INetOADB workingDB)
        {
            _workingDB = workingDB;

        }
        public bool AddImage(Image image)
        {
            var result = _workingDB.Execute("insert into users(userid,size,filename,filePath,suffix,fileFullName) values(@userid,@size,@filename,@filePath,@suffix,@fileFullName)", new { userid=image.userId, size=image.Size, filename=image.fileName, filePath=image.fileName, suffix=image.suffix, fileFullName=image.fileFullName });
            return result > 0;

        }
    }
}
