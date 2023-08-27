using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using NetOa.core;
using NetOa.Dapper.DataModel;
using NetOa.Dapper.IRepository;
using NetOa.Dapper.Repository;

namespace NetOa.Controllers
{
    public class UploadController : Controller
    {
        /// <summary>
        /// 图片仓储
        /// </summary>
        readonly IImageRepository _imageRepository;
        private IHostingEnvironment hostingEnv;
        public UploadController(IImageRepository imageRepository, IHostingEnvironment env)
        {
            _imageRepository = imageRepository;
            this.hostingEnv = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost()]
        public  IActionResult UploadImg() {
            var files = Request.Form.Files;
            ////文件大小
            long size = files.Sum(f => f.Length);
            Dictionary<String, String> fielInfo = new Dictionary<string, string>();
            List<string> list = new List<string>();
            foreach (var file in files)
            {
                //文件名称
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                
                fielInfo.Add("filename", fileName);
                //文件保存路径
           
                //string filePath = hostingEnv.WebRootPath + $@"/upload/"+ DateTime.Now.ToString("yyyyMM")+ $@"/";
                string filePath = hostingEnv.WebRootPath + "\\uploads\\" + DateTime.Now.ToString("yyyyMM") + "\\";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                fielInfo.Add("filePath", filePath);

             

                //文件后缀
                string suffix = fileName.Split('.')[1];
                string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };
               
                fielInfo.Add("suffix", suffix);
                if (!pictureFormatArray.Contains(suffix)) {
                    throw new Exception("上传文件格式不正确");

                }

                //文件名字
                fileName = Guid.NewGuid() + "." + suffix;
                fielInfo.Add("fileName", fileName);

                string fileFullName = filePath + fileName;
                //写入流文件
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                fielInfo.Add("fileFullName", fileFullName);

                string src = "/uploads/" + DateTime.Now.ToString("yyyyMM") +"/"+ fileName;
               
                fielInfo.Add("src", src);
                list.Add(src);

            }
           
           


            return Json(new { errno=0,data= list });
        }
        public class TmpUrl {

        }
    }
}