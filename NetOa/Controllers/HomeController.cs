using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetOa.core;
using NetOa.Models;
using NetOA.Dapper.DataModel;
using NetOA.Dapper.IRepository;
using NetOA.Dapper.Repository;

namespace NetOa.Controllers
{
    [Authorize(Roles = "Manager,Leader,Employee")]
    public class HomeController : Controller
    {
        /// <summary>
        /// 用户仓储
        /// </summary>
        readonly IUserRepository _userRepository;


        public HomeController( IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }



    
        public IActionResult  Index()
        {
            return View();
        }

        /// <summary>
        /// 注册视图
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
   
            try
            {
                var result = _userRepository.AddUser(user);

                return result ? backFun.success(Msg: "注册成功") : backFun.error(Msg: "注册失败，请稍后再试试");
            }
            catch (Exception ex)
            {

                return backFun.error(Msg: ex.Message);
            }

        }
        /// <summary>
        /// 登录视图
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string userName, string password, string returnUrl) {
            try
            {
                var userRole = _userRepository.Login(userName, password);
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Role,userRole.RoleName),
                    new Claim(ClaimTypes.Name,userRole.Name),
                    new Claim(ClaimTypes.Sid,userRole.ID.ToString()),
                    new Claim(ClaimTypes.GroupSid,userRole.DepartmentID.ToString()),
                };
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims)));
                return backFun.success(Msg: "登录成功",Data: userRole);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public async  Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }




        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
