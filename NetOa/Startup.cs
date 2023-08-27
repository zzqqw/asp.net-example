using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetOa.Dapper.IRepository;
using NetOa.Dapper.Repository;
using NetOA.Dapper.DataModel;
using NetOA.Dapper.IRepository;
using NetOA.Dapper.Repository;


namespace NetOa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = Configuration.GetConnectionString("SqlConntcionString");
            //services.AddDbContext<EFMySqlContext>(options =>
            //{
            //    options.UseMySql(connection);
            //});
            //services.AddSingleton(new ConfigService(Configuration.GetConnectionString("SqlConntcionString")));


            //配置跨域处理
            services.AddCors(options => options.AddPolicy("any", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowCredentials();
            }));



            //注入配置项
            //services.AddSingleton(new ConfigService(Configuration));


            //注放连接字符串
            //var connectionString = string.Format(Configuration.GetConnectionString("DefaultConnection"), System.IO.Directory.GetCurrentDirectory());
            //集成测试修改
            var connectionString = string.Format("Data Source={0}/NetOA.sqlite", System.IO.Directory.GetCurrentDirectory());
       
            //sqlieconnection注放
            services.AddSingleton(connectionString);


            //权限验证注入
            services.AddAuthentication
                (
                opts => opts.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme
                ).AddCookie(
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                opt =>
                {
                    opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login");
                    opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/home/error");
                    opt.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/login");
                    opt.Cookie.Path = "/";
                });

            //session配置
            services.AddSession();



            AddRepository(services);

            services.AddMvc();
        }



        /// <summary>
        /// 注放仓储
        /// </summary>
        /// <param name="services">服务容器</param>
        void AddRepository(IServiceCollection services)
        {
            //注放连接字符串
            //var connectionString = string.Format(Configuration.GetConnectionString("DefaultConnection"), System.IO.Directory.GetCurrentDirectory());
            //集成测试修改
            var connectionString = string.Format("Data Source={0}/NetOA.sqlite", System.IO.Directory.GetCurrentDirectory());

            services.AddSingleton(connectionString);

            //sqlieconnection注放

            services.AddScoped<IDbConnection, SqliteConnection>();

            //注放数据库
            services.AddScoped<INetOADB, NetOADB>();


            //注入图片仓储
            services.AddScoped<IImageRepository, ImageRepository>();
            //注入通知仓储
            services.AddScoped<INoticeRepository, NoticeRepository>();

            //注入文章仓储
            services.AddScoped<IArticleRepository, ArticleRepository>();
            //注入用户仓储
            services.AddScoped<IUserRepository, UserRepository>();
            //注入部门仓储
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //注入工作仓储
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();
            //注放角色仓储
            services.AddScoped<IRoleRepository, RoleRepository>();


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {




            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //开启验证中间件
            app.UseAuthentication();
            //开始session
            app.UseSession();
            //静态文件引入
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
               
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }

    }
}
