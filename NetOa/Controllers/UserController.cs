﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetOa.core;
using NetOA.Dapper.DataModel;
using NetOA.Dapper.Repository;

namespace NetOa.Controllers
{
    [Authorize(Roles = "Manager,Leader,Employee")]
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}