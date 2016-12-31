using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Common;
using Models.SysModels;


namespace Services.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Services.ApplicationDb";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            #region ��ҵ

            var sysEnterprises = new[]
            {
                new SysEnterprise()
                {
                    Id="defalutEnt",
                    EnterpriseName = "ϵͳĬ����ҵ"
                },

            };
            context.SysEnterprises.AddOrUpdate(a => new { a.Id }, sysEnterprises);



            #endregion

            #region SysArea

            var sysAreas = new[]
            {
                new SysArea
                {
                    AreaName = "Platform",
                    Name = "����ƽ̨",
                    SystemId = "002"
                }
            };
            context.SysAreas.AddOrUpdate(a => new { a.AreaName, a.Name, a.SystemId }, sysAreas);

            #endregion

            #region SysAction

            var sysActions = new[]
            {
                new SysAction
                {
                    Name = "�б�",
                    ActionName = "Index",
                    SystemId = "001",
                    System = true
                },
                new SysAction
                {
                    Name = "��ϸ",
                    ActionName = "Details",
                    SystemId = "003",
                    System = true
                },
                new SysAction
                {
                    Name = "�½�",
                    ActionName = "Create",
                    SystemId = "004",
                    System = true
                },
                new SysAction
                {
                    Name = "�༭",
                    ActionName = "Edit",
                    SystemId = "005",
                    System = true
                },
                new SysAction
                {
                    Name = "ɾ��",
                    ActionName = "Delete",
                    SystemId = "006",
                    System = true
                },
                new SysAction
                {
                    Name = "����",
                    ActionName = "Report",
                    SystemId = "007",
                    System = true
                },
            };
            context.SysActions.AddOrUpdate(a => new { a.ActionName }, sysActions);

            #endregion

            #region SysController

            var sysControllers = new[]
            {

                #region �������� 100
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "��½����ƽ̨",
                    ControllerName = "Index",
                    SystemId = "100",
                    Display = false
                },

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "����",
                    ControllerName = "Desktop",
                    SystemId = "100100",
                    Display = false
                },

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "ʹ�ð���",
                    ControllerName = "Help",
                    SystemId = "100200",
                    Display = false
                },

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "��Ϣ����",
                    ControllerName = "MyMessage",
                    SystemId = "100300",
                    Display = false
                },

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "�޸�����",
                    ControllerName = "ChangePassword",
                    SystemId = "100400",
                    Display = false
                },
                    new SysController
                                {
                                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                                    Name = "�����޸�",
                                    ControllerName = "SysUserChangePassword",
                                    SystemId = "900500",
                                    Display=false,
                                    Ico = "fa-dot-circle-o"
                                },
                #endregion other                


                #region �û����� 900
                                new SysController
                                {
                                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                                    Name = "�û�����",
                                    ControllerName = "Index",
                                    SystemId = "900",
                                    Ico = "fa-users",
                                    Display = true
                                },

                                 new SysController
                                {
                                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                                    Name = "��֯�ܹ�",
                                    ControllerName = "SysDepartment",
                                    SystemId = "900200",
                                    Ico = "fa-dot-circle-o"
                                },

                                new SysController
                                {
                                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                                    Name = "��ɫ����",
                                    ControllerName = "SysRole",
                                    SystemId = "900300",
                                    Ico = "fa-dot-circle-o"
                                },

                                new SysController
                                {
                                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                                    Name = "�û�����",
                                    ControllerName = "SysUser",
                                    SystemId = "900400",
                                    Ico = "fa-dot-circle-o"
                                },
                                   
             
             
             
                             #endregion

                
                #region ϵͳ�������� 950
                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "ϵͳ����",
                    ControllerName = "Index",
                    SystemId = "950",
                    Ico="fa-cog"
                },

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "��������",
                    ControllerName = "SysAction",
                    SystemId = "950300",
                    Ico="fa-th-large"
                },

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "ϵͳģ��",
                    ControllerName = "SysController",
                    SystemId = "950400",
                    Ico="fa-puzzle-piece"
                },

                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "������Ϣ",
                    ControllerName = "SysHelp",
                    SystemId = "950900",
                    Ico="fa-info-circle"
                },

                  new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "������Ϣ����",
                    ControllerName = "SysHelpClass",
                    SystemId = "950950",
                    Ico="fa-info-circle"
                },


                new SysController
                {
                    SysAreaId = sysAreas.Single(a => a.AreaName == "Platform").Id,
                    Name = "�û���־",
                    ControllerName = "SysUserLog",
                    SystemId = "950990",
                    Ico = "fa-calendar"
                },

#endregion

            };

            context.SysControllers.AddOrUpdate(
                a => new { a.SysAreaId, a.SystemId }, sysControllers);
            #endregion

            #region SysControllerSysAction

            var sysControllerSysActions = (from sysAction in sysActions.Where(a => a.System)
                                           from sysController in sysControllers
                                           select
                                               new SysControllerSysAction
                                               {
                                                   SysActionId = sysAction.Id,
                                                   SysControllerId = sysController.Id
                                               }).ToArray();

            context.SysControllerSysActions.AddOrUpdate(a => new { a.SysActionId, a.SysControllerId },
                sysControllerSysActions);

            #endregion


            #region ģ������Action

            context.SysControllerSysActions.AddOrUpdate(a => new { a.SysActionId, a.SysControllerId }, new SysControllerSysAction[]
            {
                 //new SysControllerSysAction{
                 //   SysActionId = sysActions.First(a=>a.ActionName=="test").Id,
                 //   SysControllerId = sysControllers.First(a=>a.ControllerName== "MyMessage").Id},

                 //new SysControllerSysAction{
                 //   SysActionId = sysActions.First(a=>a.ActionName=="test1").Id,
                 //   SysControllerId = sysControllers.First(a=>a.ControllerName== "MyMessage").Id},

                 //   new SysControllerSysAction{
                 //   SysActionId = sysActions.First(a=>a.ActionName=="test2").Id,
                 //   SysControllerId = sysControllers.First(a=>a.ControllerName== "MyMessage").Id}

            });

            #endregion

            #region ��ɫ����
            //�Զ�������Ҫ��Ȩ�����ù���
            var ids4SysControllerSysActions = sysControllerSysActions.Select(a => a.Id).ToList();
            context.SysRoleSysControllerSysActions.RemoveRange(context.SysRoleSysControllerSysActions.Where(rc => !ids4SysControllerSysActions.Contains(rc.SysControllerSysActionId)).ToList());
            //context.Commit(); ʵʱ�ύһ���Ƿ��ܽ���ظ������⣿
            foreach (var ent in sysEnterprises)
            {
                //����ϵͳ����Ա
                var sysRole = new SysRole
                {
                    Id = "admin-" + ent.Id,
                    Name = ent.EnterpriseName + "ϵͳ����Ա",
                    RoleName = "ϵͳ����Ա",
                    SysDefault = true,
                    SystemId = "999",
                    EnterpriseId = ent.Id,
                    //ϵͳ����Ա�Զ��������Ȩ��
                    //SysRoleSysControllerSysActions = (from aa in sysControllerSysActions select new SysRoleSysControllerSysAction { SysControllerSysActionId = aa.Id, RoleId = "admin-" + ent.Id }).ToArray()
                };
                context.SysRoles.AddOrUpdate(sysRole);
                //ϵͳ����Ա�Զ��������Ȩ��
                var sysRoleSysControllerSysActions = (from aa in sysControllerSysActions
                                                      select
                                                          new SysRoleSysControllerSysAction()
                                                          {
                                                              SysControllerSysActionId = aa.Id,
                                                              RoleId = sysRole.Id
                                                          }).ToArray();
                context.SysRoleSysControllerSysActions.AddOrUpdate(rc => new { rc.RoleId, rc.SysControllerSysActionId }, sysRoleSysControllerSysActions);
            }
            #endregion

        }
    }
}