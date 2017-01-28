﻿using System.Web.Optimization;
using System.Data.Entity;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Services;
using Web.Helpers;
using Fissoft.EntityFramework.Fts;

namespace Web
{
    public class WebApiApplication : HttpApplication
    {
        private Timer _objTimer;


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Run();


            //更新数据库到最新的版本
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Services.Migrations.Configuration>());


            //计划任务 按照间隔时间执行

            var onTimedEvent = DependencyResolver.Current.GetService<IOnTimedEvent>();

            _objTimer = new Timer { Interval = 10 * (1000 * 60) };
            _objTimer.Elapsed += (source, elapsedEventArgs) => onTimedEvent.Run(source, elapsedEventArgs);
            _objTimer.Start();
        }

        protected void Application_End()
        {
            _objTimer.Stop();
        }

        /// <summary>
        /// 按照用户Id更新缓存信息，不同用户Id分别缓存 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg.ToLower() == "userid")
            {
                var userid = "wjw1";

                if (context.User.Identity.IsAuthenticated)
                {
                    userid = context.User.Identity.GetUserId();
                }

                context.Trace.Write("缓存过滤器拦截", userid);

                return userid;
            }
            return base.GetVaryByCustomString(context, arg);
        }
    }
}
