﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace NetdiskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 测试代码
            //TopMuem topMuem = new TopMuem();
            //topMuem.Stratmuem();
            //ConsonlenList consonlenList = new ConsonlenList();
            //Shell shell = consonlenList.PWSComand();
            //Console.WriteLine(shell.ShellMountNetDisk);
            //Console.WriteLine(shell.ShellUnmountNetdisk);
            //InvokePWshell invokePWshell = new InvokePWshell();
            //invokePWshell.MountDiskShell();
            //invokePWshell.UnmountDiskShell();
            //Conndetail conndetail = new Program().InitConnet();
            //new Program().FunctionUserInitInvoke(conndetail);
            //new Program().FunctionUserLoginInvoke(conndetail);
            //new Program().FunctionprojectLists(conndetail);
            //CMDScript script = new CMDScript();
            //script.MountNetDiskScript(@"\\127.0.0.1\测试项目");
            //script.Cmditem();
            #region redis测试代码
            //Conndetail conndetail = new Conndetail();
            //conndetail.ServerHost = "127.0.0.1";
            //RedisModel redisModel = new RedisModel();
            //var redisc= redisModel.connectionRedis(conndetail);
            //redisModel.SetLoginStatus("test1", redisc);
            #endregion
            #region 测试项目列表生产功能
            //NetCarModel carMac = new NetCarModel();
            //Conndetail conndetail = new Program().InitConnet();
            //NetCarInfo netCarInfo = carMac.GetNetCarMac();
            ////SqlAction sql = new SqlAction();
            //FunctionProject project = new FunctionProject();
            //var connredis = project.connectionRedis(conndetail);
            //var connsql = project.ConneSql(conndetail);
            ////project.SetLoginStatus("test1", connredis, netCarInfo.NetCarMac);
            //string name = project.GetLoginStatus(netCarInfo.NetCarMac, connredis);
            //var projectlist= project.GenerateProjectList(connsql, connredis, name);
            //Console.WriteLine();
            //foreach (var item in projectlist)
            //{
            //    if (item.ftid!=0)
            //    {
            //        Console.WriteLine($"有权限访问的项目是{item.ftid}++{item.ProjectName}");
            //        string path= project.GenerateProjectPath(connsql, item.ftid);
            //        //Console.WriteLine("网盘路径{0}",path);
            //    }
            //}
            #endregion
            #region 测试菜单功能
            Conndetail conndetail = new Program().InitConnet();
            MenuAction menuAction = new MenuAction();
            SqlAction sql = new SqlAction();
            var connetsql = sql.ConneSql(conndetail);
            var connetredis = sql.connectionRedis(conndetail);
            menuAction.ChooseFunction(connetredis, connetsql);
            #endregion
            #endregion
            //while (true)
            //{
            //    new Program().MenuAction();
            //}

            //Console.ReadKey();
        }
        /// <summary>
        /// 数据库连接初始化
        /// </summary>
        /// <returns>返回连接初始化信息对象</returns>
        Conndetail InitConnet()
        {
            Conndetail conndetail = new Conndetail();
            conndetail.ServerHost = "10.12.2.61";
            //conndetail.ServerHost = "127.0.0.1";
            conndetail.DBName = "TestDemo";
            conndetail.DbUser = "sa";
            conndetail.DbPassword = "sa";
            return conndetail;
        }
        /// <summary>
        /// 主菜单操作逻辑
        /// </summary>
        //static UserInfo loginUserinfo = new UserInfo();
        //void MenuAction()
        //{
        //    NetCarModel carMac = new NetCarModel();
        //    Conndetail conndetail = new Program().InitConnet();
        //    ClineMuemModel topMuem = new ClineMuemModel();
        //    CMDScript cmdscript = new CMDScript();
        //    RedisModel redis = new RedisModel();
        //    var db= redis.connectionRedis(conndetail);
        //    NetCarInfo netCarInfo= carMac.GetNetCarMac();
        //    string loginstatus= redis.GetLoginStatus(netCarInfo.NetCarMac, db);
        //    if (loginstatus==null)
        //    {
        //        #region 第一次登录的页面
        //        int changecode = topMuem.Stratmuem();
        //        if (changecode == 1)
        //        {
        //            int loginCode = FunctionUserLoginInvoke(conndetail, loginUserinfo);
        //            if (loginCode == 1)
        //            {

        //                Console.Clear();
        //                redis.SetLoginStatus(loginUserinfo.UserName, db, netCarInfo.NetCarMac);
        //                Console.WriteLine("登录成功");
        //                Console.WriteLine();
        //                int code = FunctionprojectLists(conndetail);
        //                string path = FunctionprojectPath(conndetail, code);
        //                //挂载网盘
        //                //new InvokePWshell().MountDiskShell(path);
        //                string cmdcls = cmdscript.MountNetDiskScript(path);
        //                cmdscript.RunCMDscript(cmdcls);
        //                Console.Clear();

        //            }
        //            else
        //            {
        //                Console.Clear();
        //                Console.WriteLine("登录失败，重新登录");
        //            }

        //        }
        //        else if (changecode == 8)
        //        {
        //            Console.Clear();
        //            Console.Write("请输入要删除的盘符：");
        //            string inputitem = Console.ReadLine();
        //            string cmd = cmdscript.UnMountNetDiskScript(inputitem);
        //            cmdscript.RunCMDscript(cmd);
        //            Console.Clear();

        //        }
        //        else if (changecode == 9)
        //        {
        //            FunctionUserInitInvoke(conndetail, loginUserinfo);
        //            Console.Clear();
        //        }
        //        if (changecode == 0)
        //        {
        //            Environment.Exit(0);
        //        }
        //        #endregion
        //    }
        //    else if (loginstatus!=null)
        //    {
        //        Console.Clear();
        //        #region 非第一次登录的页面
        //        topMuem.SecondMenu();

        //        int code = FunctionprojectLists(conndetail);

        //        if (code!=8)
        //        {
        //            string path = FunctionprojectPath(conndetail, code);
        //            //挂载网盘
        //            //new InvokePWshell().MountDiskShell(path);
        //            string cmdcls = cmdscript.MountNetDiskScript(path);
        //            cmdscript.RunCMDscript(cmdcls);
        //        }
        //        else if (code==8)
        //        {
        //            Console.Clear();
        //            Console.Write("请输入要删除的盘符：");
        //            string inputitem = Console.ReadLine();
        //            string cmd = cmdscript.UnMountNetDiskScript(inputitem);
        //            cmdscript.RunCMDscript(cmd);
        //            Console.Clear();
        //        }
        //        #endregion
        //    }

        //}
        /// <summary>
        /// 调用用户初始化方法
        /// </summary>
        /// <param name="conndetail">sql连接对象</param>
        //void FunctionUserInitInvoke(Conndetail conndetail, UserInfo loginUserinfo)
        //{
        //    SqlAction sqlAction = new SqlAction();
        //    var connet = sqlAction.ConneSql(conndetail);
        //    InitUser initUser = new InitUser();           
        //    UserInfo userInfo= initUser.Init(loginUserinfo);
        //    sqlAction.UserInsert(userInfo, connet);
        //}
        /// <summary>
        /// 调用用户登录方法
        /// </summary>
        /// <param name="conndetail">sql连接对象</param>
        //int FunctionUserLoginInvoke(Conndetail conndetail, UserInfo loginUserinfo)
        //{
        //    SqlAction sqlAction = new SqlAction();
        //    var connet = sqlAction.ConneSql(conndetail);
        //    InitUser initUser = new InitUser();
        //    UserInfo userInfoLogin = initUser.LoginUserInfo(loginUserinfo);
        //    string cmd = sqlAction.SelectUser(userInfoLogin.UserName, userInfoLogin.Password);
        //    var dt= sqlAction.SeletScript(cmd, connet);
        //    if (dt!=null)
        //    {
        //        Console.WriteLine("用户名密码正确，登录成功");
        //        return dt.Rows.Count;
        //    }
        //    else
        //    {
        //        Console.WriteLine("用户名或密码错误，请重新登录");
        //        return dt.Rows.Count;
        //    }
        //读取内存对象中的查询语句
        //if (dt != null)
        //{
        //    //读出DataTable中的数据
        //    for (int i = 0; i < dt.Rows.Count; i++) //行
        //    {
        //        for (int j = 0; j < dt.Columns.Count; j++) //列
        //        {
        //            Console.Write(dt.Rows[i][j]);
        //            Console.Write("\t");
        //        }
        //        Console.WriteLine();
        //    }
        //}
        //Console.WriteLine(dt.Rows.Count);
        //}

        /// <summary>
        /// 调用项目列表读取功能，查询数据库，显示项目列表
        /// </summary>
        /// <param name="conndetail">sql连接对象</param>
        /// <returns>返回用户输入的项目编号</returns>
        //int FunctionprojectLists(Conndetail conndetail)
        //{
        //    SqlAction sqlAction = new SqlAction();
        //    ProjectMenuListInfo promenu = new ProjectMenuListInfo();
        //    List<ProjectList> projectmenuLists = new List<ProjectList>();
        //    var connet = sqlAction.ConneSql(conndetail);
        //    string cmd = sqlAction.SelectDisk();
        //    var dt = sqlAction.SeletScript(cmd, connet);
        //    if (dt!=null)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            ProjectList projectmenu = new ProjectList();
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                if (j%2==0)
        //                {
        //                    projectmenu.FolderName = dt.Rows[i][j].ToString();
        //                    //Console.Write(projectmenu.FolderName);
        //                    //Console.Write("\t");
        //                }                        
        //                else if (j%2==1)
        //                {
        //                    projectmenu.ftid = int.Parse(dt.Rows[i][j].ToString());
        //                    //Console.Write(projectmenu.ftid);
        //                }

        //                //Console.Write(dt.Rows[i][j]);
        //                //Console.Write("\t");
        //                //Console.WriteLine(j%2);
        //            }
        //            projectmenuLists.Add(projectmenu);
        //            //Console.WriteLine();
        //        }                                
        //    }
        //    return promenu.Projectmenu(projectmenuLists);
        //}
        /// <summary>
        /// 根据数据库查询结果，生成项目连接路径
        /// </summary>
        /// <param name="conndetail">sql连接对象</param>
        /// <param name="projectcode">查询到的项目代码</param>
        /// <returns>生成的项目路径</returns>
        //string FunctionprojectPath(Conndetail conndetail,int projectcode)
        //{
        //    SqlAction sqlAction = new SqlAction();
        //    var connet = sqlAction.ConneSql(conndetail);
        //    string cmd = sqlAction.SelectProjectCode(projectcode);
        //    var dt = sqlAction.SeletScript(cmd, connet);
        //    if (dt!=null)
        //    {                
        //        string remotePath = Path.Combine("10.12.2.19", dt.Rows[0][0].ToString());//远程主机IP
        //        remotePath = String.Format($@"\\{remotePath}");
        //        Console.WriteLine(@"查询到的项目路径为{0}",remotePath);
        //        return remotePath;
        //    }
        //    else
        //    {
        //        Console.WriteLine("项目编号输入错误");
        //        return null;
        //    }
        //}


    }
}
