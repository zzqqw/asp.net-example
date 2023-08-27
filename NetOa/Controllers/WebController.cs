using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetOa.core;
using NetOa.Dapper;
using NetOa.Dapper.DataModel;
using NetOa.Dapper.IRepository;
using NetOA.Dapper.DataModel;
using NetOA.Dapper.IRepository;

namespace NetOa.Controllers
{
    [Microsoft.AspNetCore.Cors.EnableCors("any")]
    public class WebController : Controller
    {
        /// <summary>
        /// 用户仓储
        /// </summary>
        readonly IUserRepository _userRepository;
        /// <summary>
        /// 部门仓储
        /// </summary>
        readonly IDepartmentRepository _departmentRepository;
        /// <summary>
        /// 文章仓储
        /// </summary>
        readonly IArticleRepository _articleRepository;
        /// <summary>
        /// 角色仓储
        /// </summary>
        readonly IRoleRepository _roleRepository;
        /// <summary>
        /// 工作仓储
        /// </summary>
        readonly IWorkItemRepository _workItemRepository;

        readonly INoticeRepository _noticeRepository;

        private IHostingEnvironment hostingEnv;
        public WebController(
            IUserRepository userRepository,
            IDepartmentRepository departmentRepository,
            IArticleRepository articleRepository,
            IRoleRepository roleRepository, 
            IWorkItemRepository workItemRepository,
            INoticeRepository noticeRepository,
            IHostingEnvironment env
            )
        {

            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _articleRepository = articleRepository;
            _roleRepository = roleRepository;
            _workItemRepository = workItemRepository;
            _noticeRepository = noticeRepository;
            this.hostingEnv = env; 
        }

        public IActionResult Index(){
            return backFun.success(Msg: "This is api");
        }

        public IActionResult GetIsReadNotice(Search search) {

            try
            {
                //search.userId = 1;
                var result = _noticeRepository.GetIsReadNotice(search);
                return backFun.success(Msg: "根据发起人获取通知", Data: result);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
        /// <summary>
        /// 根据发起人获取通知
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult GetNoticeByFormUserID(Search search)
        {
            try
            {
                //search.userId = 1;
                var result = _noticeRepository.GetNoticeByFormUserID(search);
                return backFun.success(Msg: "根据发起人获取通知", Data: result);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 根据接收人获取通知
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult GetNoticeByToUserID(Search search) {
            try
            {
                //search.userId = 1;
                var result = _noticeRepository.GetNoticeByToUserID(search);
                return backFun.success(Msg: "根据接收人获取通知", Data: result);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 对通知进行标记
        /// </summary>
        /// <param name="isReadNotice"></param>
        /// <returns></returns>
        public IActionResult ChageNotice(IsReadNotice isReadNotice)
        {
            try
            {
                //isReadNotice.UserID = 2;
                //isReadNotice.statusMsg = "已读";
                //isReadNotice.Remarks = "成功已读";
                //isReadNotice.NoticeID = "1,2,3,4,5";

                var result = _noticeRepository.AddIsReadNotices(isReadNotice);
                return result > 0 ? backFun.success(Msg: "成功标记已读", Data: isReadNotice) : backFun.error("您已经标记过了");
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 添加通知
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public IActionResult AddNotice(Notice notice)
        {
            try
            {
                //notice.NoticeType = "请假";
                //notice.NoticeTitle = "请假标题";
                //notice.NoticeContent = "请假内容";
                //notice.FormUserID = notice.FormUserID > 0 ? notice.FormUserID : 2;


                notice.ToUserID = notice.ToUserID.GetType() == typeof(int) ? notice.ToUserID.ToString() : notice.ToUserID;

                var result = _noticeRepository.AddNotice(notice);
                return result ? backFun.success(Msg: "通知添加成功", Data: notice) : backFun.error(Msg: "通知添加失败，请稍后再试试");

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
        /// <summary>
        /// 修改通知
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public IActionResult EdiNotice(Notice notice)
        {
            try
            {
                //notice.ID = 1;
                //notice.NoticeType = "请假2222";
                //notice.NoticeTitle = "请假标题";
                //notice.ToUserID = "1,2,3";
                //notice.NoticeContent = "请假内容";
                var result = _noticeRepository.ModifyNotice(notice);
                return result ? backFun.success(Msg: "通知编辑成功", Data: notice) : backFun.error(Msg: "通知编辑失败，请稍后再试试");

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public IActionResult delNotice(Notice notice)
        {

            try
            {
                var result = _noticeRepository.RemoveNotice(notice);
                return result ? backFun.success(Msg: "通知删除成功", Data: notice) : backFun.error(Msg: "通知删除失败，请稍后再试试");

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }


        /// <summary>
        /// 根据文章ID获取评论
        /// </summary>
        /// <param name="ArticlesID"></param>
        /// <returns></returns>
        public IActionResult QueryPinglun(int ArticlesID)
        {

            try
            {
                var result = _articleRepository.GetPinglunByArticlesID(ArticlesID);
                return backFun.success(Msg: "根据文章ID获取评论成功", Data: result);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="PinglunID"></param>
        /// <returns></returns>
        public IActionResult delPinglun(int PinglunID)
        {
            try
            {
                var result = _articleRepository.RemovePinglun(PinglunID);
                return result ? backFun.success(Msg: "评论删除成功", Data: PinglunID) : backFun.error(Msg: "评论删除失败，请稍后再试试");

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="pinglun"></param>
        /// <returns></returns>
        public IActionResult AddPinglun(Pinglun pinglun) {

            try
            {
                //pinglun.UserID = 2;
                //pinglun.ArticlesID = 1;
                //pinglun.content = "123456";
                pinglun.CreateTime = DateTime.Now;
                var result= _articleRepository.AddPinglun(pinglun);
                return result ? backFun.success(Msg: "评论添加成功", Data: pinglun) : backFun.error(Msg: "评论添加失败，请稍后再试试");

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }


        /// <summary>
        /// 获取作业
        /// </summary>
        /// <param name="type"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IActionResult GetWorkItem(int type ,Search search)
        {

            try
            {
                var WorkItem = _workItemRepository.GetWorkItem(type, search);
                return backFun.success(Msg: "获取作业成功", Data: WorkItem);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
        /// <summary>
        /// 修改作业
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public IActionResult ModifyWorkItem(WorkItem workItem)
        {

            try
            {
                //workItem.Area = "heibei";
                //workItem.Latitude = 10.123415456;
                //workItem.Longitude = 12.4564251;
                //workItem.Remarks = "1231231das";
                //workItem.ID = 4;
                //return Json(workItem);
               
                var result = _workItemRepository.ModifyWorkItem(workItem);
                return result ? backFun.success(Msg: "作业修改成功",Data: workItem) : backFun.error(Msg: "修改失败，请稍后再试试");

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }


        /// <summary>
        /// 添加风险作业
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public IActionResult AddWorkItem(WorkItem workItem ) {

            try
            {

                //workItem.Title = "作业标题3";
                //workItem.Area = "河南";
                //workItem.Latitude = 10.1234154564564;
                //workItem.Longitude = 12.45642513124213;
                //workItem.WorkContent = "作业内容";
                //workItem.Type = 1;
                //workItem.Remarks = "作业备注信息2";
                //workItem.RecordDate = "2018-12-12:12";
                //workItem.CreateUserID = 2;
                //workItem.ReviewerUserID = 4;
                //workItem.Status = 1;

                workItem.CreateTime = DateTime.Now;
                var result = _workItemRepository.AddWorkItem(workItem);
                return result ? backFun.success(Msg: "添加作业成功",Data: workItem) : backFun.error(Msg: "添加失败，请稍后再试试");

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
           
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        //[HttpPost]
        public IActionResult Login(string userName, string password)
        {
            try
            {
                var userRole = _userRepository.Login(userName, password);
                var BuildToken = Token.BuildToken(
                    new Dictionary<string, object> {
                     { "userid", userRole.ID },
                     { "username", userRole.Name },
                     { "phone", userRole.Phone }
                 });
                HttpContext.Session.SetString("token", BuildToken);
                return backFun.success(Msg: "登录成功", Data: new { token = BuildToken,userId= userRole.ID,userInfo= userRole });
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }

        /// <summary>
        /// 按部门ID获取用户
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetUserByDepartmentID(int departmentID)
        {
            try
            {
                var users = _userRepository.GetUsersByDepartmentID(departmentID);

                return backFun.success(Msg: "根据部门id获取用户信息成功", Data: users);

            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }


        /// <summary>
        /// 查询全部角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _roleRepository.GetRoles();
                return backFun.success(Msg: "获取角色成功", Data: roles);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
       /// <summary>
       /// 获取全部部门
       /// </summary>
       /// <returns></returns>
        [HttpPost]
        public IActionResult GetDepartment()
        {
            try
            {
                var Department = _departmentRepository.GetAllDepartment();
                return backFun.success(Msg: "获取部门成功", Data: Department);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveUser(int userID)
        {

            try
            {
                var result = _userRepository.RemoveUser(userID);
                return result ? backFun.success(Msg: "删除成功") : backFun.error(Msg: "删除失败，请稍后再试试");
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IActionResult QueryUser(Search search)
        {
            try
            {

                var user = _userRepository.GetUserList(search);
                return backFun.success(Msg: "获取成功", Data: user);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ModifyUser(User user)
        {
            //return Json(user);
            try
            {
                var result = _userRepository.ModifyUser(user);

                return result ? backFun.success(Msg: "修改成功") : backFun.error(Msg: "修改失败，请稍后再试试");
            }
            catch (Exception exc)
            {

                return backFun.error(Msg: exc.Message);
            }
        }


        /// <summary>
        /// 获取所有的文章
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        public IActionResult getallArticle(int type, Search search)
        {
            //return Json(search);
            try
            {

                var ArticleList = _articleRepository.GetAllArticle(type, search);
                return backFun.success(Msg: "获取" + type + "列表成功", Data: ArticleList);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getallColumn()
        {
            try
            {
                var data = _articleRepository.GetAllColumn();
                return backFun.success(Msg: "获取栏目列表成功", Data: data);
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditArticle(Article article)
        {
            //return Json(article);
            try
            {
                var result = _articleRepository.EditArticle(article);
                return result ? backFun.success(Msg: "修改成功") : backFun.error(Msg: "修改失败，请稍后再试试");
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }


        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleid"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DelArticle(int articleid)
        {
            try
            {
                var result = _articleRepository.RemoveArticle(articleid);
                return result ? backFun.success(Msg: "删除成功") : backFun.error(Msg: "删除失败，请稍后再试试");
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 添加文章操作
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddArticle(Article article)
        {
            try
            {
                article.CreateTime = DateTime.Now;
                if (article.CreateUserID!=0) {
                    int.TryParse(User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid)?.Value, out int userID);
                    article.CreateUserID = userID;
                }
                var result = _articleRepository.AddArticle(article);
                return result ? backFun.success(Msg: "添加成功") : backFun.error(Msg: "添加失败，请稍后再试试");
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }
        }
        /// <summary>
        /// 文件上传接口
        /// </summary>
        /// <returns></returns>
        public IActionResult Upload()
        {

            try
            {
                var files = Request.Form.Files;
                if (files.Count() < 1)
                {
                    return backFun.error(Msg: "请选择上传文件");
                }
                else if (files.Count() > 1)
                {
                    return backFun.error(Msg: "当前接口只支持单文件上传");
                }
                else
                {
                    Dictionary<String, String> keyValues = new Dictionary<string, string>();
                    long size = files.Sum(f => f.Length);
                    foreach (var file in files)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string WebRootPath = hostingEnv.WebRootPath;
                        string Path = WebRootPath + "\\uploads\\" + DateTime.Now.ToString("yyyyMM") + "\\";
                        if (!Directory.Exists(WebRootPath + Path))
                        {
                            Directory.CreateDirectory(WebRootPath + Path);
                        }

                        //文件后缀
                        string suffix = fileName.Split('.')[1];
                        //文件名字
                        //var saveFileName = Guid.NewGuid() + "." + suffix;
                        Random rd = new Random();
                        var saveFileName = DateTime.UtcNow.ToString("yyyy" + "MM" + "dd" + "HH" + "mm" + "ss" + "ffffff") + rd.Next(9, 999) + "." + suffix;
                        string fileFullName = WebRootPath + Path + saveFileName;
                        //写入流文件
                        using (FileStream fs = System.IO.File.Create(fileFullName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        keyValues.Add("saveFileName", saveFileName);
                        keyValues.Add("fileName", fileName);
                        keyValues.Add("suffix", suffix);
                        string src = "/uploads/" + DateTime.Now.ToString("yyyyMM") + saveFileName;
                        keyValues.Add("src", src);
                    }
                    return backFun.success(Msg: "上传成功", Data: keyValues);


                }
            }
            catch (Exception ex)
            {
                return backFun.error(Msg: ex.Message);
            }

        }
    }
}