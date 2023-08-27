using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NetOa.Controllers
{
    [Authorize(Roles = "Manager,Leader,Employee")]
    public class ArticleController : Controller
    {

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult New()
        {
            return View();
        }
        public IActionResult Legal()
        {
            return View();
        }
        public IActionResult Bbs()
        {
            return View();
        }


    }
}