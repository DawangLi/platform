﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFramework.Caching;
using EntityFramework.Extensions;
using IServices.ISysServices;
using Models.SysModels;

namespace Web.Helpers
{
    /// <summary>
    /// 用户身份验证
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 需要验证的区域   
        /// </summary>
        public IList<string> Areas { private get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var area = (string)httpContext.Request.RequestContext.RouteData.DataTokens["area"];

            //是否对该Area区域进行身份验证
            if (!Areas.Contains(area)) return true;

            //判断是否用户已登录
            if (!httpContext.User.Identity.IsAuthenticated) return false;

            //判断用户是否有该区域访问的权限
            //如果权限中有该区域的任何一个操作既可以进行访问

            var action = (string)httpContext.Request.RequestContext.RouteData.Values["action"];
            var controller = (string)httpContext.Request.RequestContext.RouteData.Values["controller"];
            var recordId = (string)httpContext.Request.RequestContext.RouteData.Values["id"];

            var sysRoleService = DependencyResolver.Current.GetService<ISysRoleService>();
            var userInfo = DependencyResolver.Current.GetService<IUserInfo>();

            //检测用户是否有权限访问 

            if (!sysRoleService.CheckSysUserSysRoleSysControllerSysActions(userInfo.UserId, area, action, controller))
                throw new Exception("用户：" + userInfo.UserName + "(" + userInfo.UserId + ") 没有权限访问 " + area + " > " + controller + " > " + action + " ！请联系系统管理员进行权限分配！");


            //记录用户访问记录

            var sysControllerSysActionService = DependencyResolver.Current.GetService<ISysControllerSysActionService>();
            var sysUserLogService = DependencyResolver.Current.GetService<ISysUserLogService>();

            var sysControllerSysAction =
                sysControllerSysActionService.GetAll(a => a.SysController.ControllerName.Equals(controller) &&
                            a.SysController.SysArea.AreaName.Equals(area) && a.SysAction.ActionName.Equals(action)).OrderBy(a => a.SysController.SystemId).FromCacheFirstOrDefault();

            if (sysControllerSysAction == null) return true;

            var sysuserlog = new SysUserLog
            {
                Ip = httpContext.Request.ServerVariables["Remote_Addr"],
                SysControllerSysActionId = sysControllerSysAction.Id,
                RecordId = recordId,
                Url = httpContext.Request.RawUrl,
            };

            sysUserLogService.Save(null, sysuserlog);

            sysUserLogService.Commit();

            return true;
        }
    }




}