using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.core
{
    public class backFun
    {
        public static JsonResult success(string Msg = "", string Url = "/", dynamic Data = null, int Code = 1)
        {
            return toJson(Code: Code, Msg: Msg, Url: Url, Data: Data);
        }
        public static JsonResult error(string Msg = "", string Url = null, dynamic Data = null, int Code = 0)
        {
            var url = string.IsNullOrEmpty(Url) ? "javascript:history.back(-1)" : Url;
            return toJson(Code: Code, Msg: Msg, Url: url, Data: Data);
        }
        private static JsonResult toJson(int Code, string Msg, string Url, dynamic Data)
        {
            return new JsonResult(new { code = (int)Code, url = Url, msg = Msg, data = Data });
        }
    }
}
