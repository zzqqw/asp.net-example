using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using NetOa.Dapper;
using NetOa.Dapper.DataModel;
using NetOa.Dapper.DataModelView;
using NetOA.Dapper.DataModel;
using NetOA.Dapper.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace NetOA.Dapper.Repository
{
    /// <summary>
    /// 用户仓储类
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        INetOADB _workingDB;

        public UserRepository(INetOADB workingDB)
        {
            _workingDB = workingDB;

        }
        public List<UserRoleDepartment>  GetUserList(Search search) {
            string sql = "select " +
                "users.id as uid  ,users.*  , departments.* , roles.* " +
                "from users,departments,roles " +
                "where users.roleid=roles.id AND users.departmentID=departments.id";
            var p = new DynamicParameters();
            if (search.userId > 0)
            {
                sql += " AND users.id = @id";
                p.Add("id", search.userId);
            }
            if (!string.IsNullOrEmpty(search.KeyWords)) {
                sql += " AND UserName like @UserName";
                p.Add("UserName", search.KeyWords);
            }

            return _workingDB.Query<UserRoleDepartment>(sql).ToList();
         
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserRole Login(string userName, string password)
        {
            string sql = "select " +
                "users.*,roles.rolename " +
                "from users join roles on users.roleid=roles.id " +
                "where username=@username and password=@password";
            var userRole = _workingDB.Query<UserRole>(sql, new { username = userName, password = password }).SingleOrDefault();
            if (userRole == null)
            {
                throw new Exception("用户名或密码错误！");
            }
            else
            {
                return userRole;
            }

        }

        /// <summary>
        /// 按ID获取用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public User GetUser(int userID)
        {
            string sql = "select * from users where id=@id"; 
            return _workingDB.Query<User>(sql, new { id = userID }).SingleOrDefault();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPassword">新密码</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public bool ModifyPassword(string newPassword, string oldPassword, int userID)
        {
            var user = GetUser(userID);
            string sql = "update users set password=@password where id=@id";
            if (user!=null&&user.Password == oldPassword)
            {

                return _workingDB.Execute(sql, new { password = newPassword, id = userID }) > 0;
            }
            else
            {
               throw new Exception($"修改密码:修改密码失败:旧密码不正确");
            }
        }
        /// <summary>
        /// 按部门查询用户
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        public List<User> GetUsersByDepartmentID(int departmentID)
        {
            string sql = "select * from users where departmentid=@departmentid";
            return _workingDB.Query<User>(sql, new { departmentid = departmentID }).ToList();
        }
        /// <summary>
        /// 根据部门查看用户
        /// </summary>
        /// <returns></returns>
        public List<UserRole> GetDepartmentUsers(int departmentID)
        {
            string sql = "select users.*,roles.rolename from users join roles on users.roleid=roles.id where users.departmentid=@departmentid";
            return _workingDB.Query<UserRole>(sql, new { departmentid = departmentID }).ToList();

        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            if (user == null)
            {
                throw new Exception("添加的用户不能为Null");
            }
            else
            {
                
                string sql = "insert into users(roleid,departmentid,name,username,password) values(@roleid,@departmentid,@name,@username,@password)";
                user.Password = user.UserName;
                var result = _workingDB.Execute(sql, new { roleid = user.RoleID, departmentid = user.DepartmentID, name = user.Name, username = user.UserName, password = user.Password, });
                return result > 0;
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool ModifyUser(User user)
        {
            if (user == null)
            {
                throw new Exception("修改的用户不能为Null");
            }
            else
            {
 
                string sql = "update users set roleid=@roleid,departmentid=@departmentid,name=@name,username=@username,password=@password  ,Phone=@phone where id=@id";
                return _workingDB.Execute(sql, 
                    new {
                        roleid = user.RoleID,
                        departmentid = user.DepartmentID,
                        name = user.Name,
                        username = user.UserName,
                        password = user.Password,
                        phone = user.Phone,
                        id = user.ID }) > 0;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool RemoveUser(int userID)
        {
            string sql = "delete from users where id=@id";
            if (userID == 1)
            {
                throw new Exception("无法删除超级管理员，请稍后再试试");
            }
            else {
                return _workingDB.Execute(sql, new { id = userID }) > 0;
            }
        }


    }
}
