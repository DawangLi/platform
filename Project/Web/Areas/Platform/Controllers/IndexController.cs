﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityFramework.Extensions;
using IServices.Infrastructure;
using IServices.ISysServices;
using Models.SysModels;
using Services;

namespace Web.Areas.Platform.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class IndexController : Controller
    {
        private readonly ISysControllerService _sysControllerService;
        private readonly IUserInfo _iUserInfo;
        private readonly ISysHelpService _iSysHelpService;
        private readonly ISysEnterpriseSysUserService _iSysEnterpriseSysUserService;
        private readonly ISysUserService _iSysUserService;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly ISysUserLogService _iSysUserLogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysControllerService"></param>
        /// <param name="iUserInfo"></param>
        /// <param name="iSysHelpService"></param>
        /// <param name="iSysEnterpriseSysUserService"></param>
        /// <param name="iSysUserService"></param>
        /// <param name="iUnitOfWork"></param>
        /// <param name="iSysUserLogService"></param>
        public IndexController(ISysControllerService sysControllerService, IUserInfo iUserInfo, ISysHelpService iSysHelpService, ISysEnterpriseSysUserService iSysEnterpriseSysUserService, ISysUserService iSysUserService, IUnitOfWork iUnitOfWork, ISysUserLogService iSysUserLogService)
        {
            _sysControllerService = sysControllerService;
            _iUserInfo = iUserInfo;
            _iSysHelpService = iSysHelpService;
            _iSysEnterpriseSysUserService = iSysEnterpriseSysUserService;
            _iSysUserService = iSysUserService;
            _iUnitOfWork = iUnitOfWork;
            _iSysUserLogService = iSysUserLogService;
        }

        // GET: Platform/Index
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changesysenterprise"></param>
        /// <param name="loadPage"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string changesysenterprise, string loadPage = "")
        {
            var ent = _iSysEnterpriseSysUserService.GetAll(a => a.SysUserId == _iUserInfo.UserId);

            if (!string.IsNullOrEmpty(changesysenterprise))
            {
                if (ent.Any(a => a.SysEnterpriseId.Equals(changesysenterprise)))
                {
                    var user = _iSysUserService.GetById(_iUserInfo.UserId);
                    user.CurrentEnterpriseId = changesysenterprise;
                    await _iUnitOfWork.CommitAsync();

                    return RedirectToAction("Index");
                }
            }

            ViewBag.sysEnterpriseSysUser = ent;
            ViewBag.UserId = _iUserInfo.UserId;
            ViewBag.UserName = _iUserInfo.UserName;
            ViewBag.OffsiderbarHelp = _iSysHelpService.GetAll().ToListAsync().Result;
            ViewBag.LoadPage = loadPage;

            ViewBag.sysEnterprises = new SelectList(ent.Select(a => a.SysEnterprise).Future(), "Id", "EnterpriseName", _iUserInfo.EnterpriseId);

            var area = (string)RouteData.DataTokens["area"];

            ViewBag.Menu = _sysControllerService.GetAll(a =>
                a.Display && a.Enable &&
                a.SysControllerSysActions.Any(
                    b =>
                        b.SysRoleSysControllerSysActions.Any(
                            c => c.SysRole.EnterpriseId == _iUserInfo.EnterpriseId &&
                                c.SysRole.Users.Any(
                                    d => d.UserId == _iUserInfo.UserId))) &&
                a.SysArea.AreaName.Equals(area)).Future();

            //桌面统计

            //近十天用户注册次数
            ViewBag.SysUserCountDay = _iSysUserService.GetAll(a => a.CreatedDateTime > DbFunctions.AddDays(DateTimeLocal.Now.Date, -30)).GroupBy(a => a.CreatedDate).Select(a => new { a.Key, Count = a.Count() }).OrderBy(a => a.Key).ToDictionaryAsync(a => a.Key, a => (double)a.Count).Result;

            //近十天用户活动次数
            ViewBag.SysUserLogCountDay = _iSysUserLogService.GetAll().GroupBy(a => a.CreatedDate).Select(a => new { a.Key, Count = a.Count() }).OrderBy(a => a.Key).ToDictionaryAsync(a => a.Key, a => (double)a.Count).Result;

            //执行速度
            ViewBag.SysUserLogDayDuration = _iSysUserLogService.GetAll().GroupBy(a => a.CreatedDate).Select(a => new { a.Key, Duration = a.Average(b => b.Duration) }).OrderBy(a => a.Key).ToDictionaryAsync(a => a.Key, a => Math.Round(a.Duration, 3)).Result;

            return View();
        }
        

    }
}