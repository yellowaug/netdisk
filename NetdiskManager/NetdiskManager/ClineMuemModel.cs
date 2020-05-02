using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Data;
using System.Data.SqlClient;

namespace NetdiskManager
{
    

    public class ClineMuemModel
    {
        public int Stratmuem()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1:\t登\t录\t");
            Console.WriteLine("----------------------------------");
            //Console.WriteLine("8:\t退\t出\t项\t目\t");
            //Console.WriteLine();
            //Console.WriteLine("9:\t用户信息录入(第一次使用程序的时候使用)");
            //Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("0:\t退\t出\t程\t序\t");
            Console.WriteLine("----------------------------------");
            Console.Write("请选择功能(输入完成按回车进入)：");
            return int.Parse(Console.ReadLine());
            
        }
        //public void SecondMenu()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("8:\t退\t出\t项\t目\t");                        
        //}
        /// <summary>
        /// 功能菜单
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="sqlConnection">SQL连接对象</param>
        /// <param name="redisdb">REDIS连接对象</param>
        /// <param name="userMenu">用户菜单类</param>
        /// <param name="projectMenuList">项目菜单类</param>
        public void FunctionMenu(UserInfo userInfo,SqlConnection sqlConnection,IDatabase redisdb,UserMenu userMenu,ProjectMenuListInfo projectMenuList)
        {           
            int funcCode = this.Stratmuem();
            if (funcCode.ToString().Length==1)
            {
                if (funcCode == 1)
                {


                    UserInfo loguserInfo = userMenu.LoginUserInfo(userInfo);
                    FunctionUserLogin userLogin = new FunctionUserLogin();
                    int logincode = userLogin.UserLogin(sqlConnection, loguserInfo);
                    if (logincode == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("用户名密码正确，登录成功");
                        userLogin.SetUserLoginStatus(redisdb, loguserInfo.UserName);
                        projectMenuList.ChooseProjecMenu(redisdb, sqlConnection, loguserInfo.UserName);
                        #region 这个是旧的项目权限获取方法及菜单调用的写法
                        //CMDScript cmdScript = new CMDScript();
                        //FunctionProject functionProject = new FunctionProject();
                        //var list = functionProject.GenerateProjectList(sqlConnection, redisdb, loguserInfo.UserName);
                        //ProjectMenuListInfo projectMenuList = new ProjectMenuListInfo();
                        //while (true)
                        //{
                        //    int projectcode = projectMenuList.Projectmenu(list);
                        //    if (projectcode == 00000)
                        //    {
                        //        break;
                        //    }
                        //    else if (projectcode == 99999)
                        //    {
                        //        string disk = projectMenuList.UnProject();
                        //        string unmountshell = cmdScript.UnMountNetDiskScript(disk);
                        //        cmdScript.RunCMDscript(unmountshell);
                        //    }
                        //    else
                        //    {
                        //        Console.Clear();
                        //        string remotePath = functionProject.GenerateProjectPath(sqlConnection, projectcode);
                        //        string mountshell = cmdScript.MountNetDiskScript(remotePath);
                        //        cmdScript.RunCMDscript(mountshell);
                        //    }


                        //}
                        #endregion


                    }
                    else if (logincode == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("用户名密码错误，登录失败，请重新登录");
                        new MenuAction().ChooseFunction(redisdb, sqlConnection);
                    }
                }
                else if (funcCode == 0)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("请输入正确的功能编号");
                Console.WriteLine("-----------------------------------");
                this.FunctionMenu(userInfo, sqlConnection, redisdb, userMenu, projectMenuList);
            }
            
            
        }
    }
    public class UserMenu
    {
        public void NoiceInfo()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("首次登录请按提示输入用户信息");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("注意：");
            Console.WriteLine("1.输入的账号密码请勿使用自己的常用密码！！！");
            Console.WriteLine("2.请认真按提示输入信息，个人精力有限，没有学会时间管理所以许多功能的输入检查没有做，求你们别乱输入东西了，谢谢");
            Console.WriteLine("  要是乱输入信息报了未知BUG或者程序卡死请关掉软件重新来过");
            Console.WriteLine("3.软件绑定本机的网卡物理地址，请勿将软件乱拷贝");
            Console.WriteLine("4.。。。。。。。额。。。。。");
            Console.WriteLine("-------------------------------------------------");
        }
        /// <summary>
        /// 初始化用户信息，用户输入，账号密码小组号写入数据库
        /// </summary>
        /// <returns></returns>
        public UserInfo Init(UserInfo userInfo)
        {
            
            Console.WriteLine("请输入用户名(如test1,test2，请勿输入纯数字！)：");
            Console.WriteLine("-------------------------------------------------");
            userInfo.UserName= Console.ReadLine();
            Console.WriteLine("-------------------------------------------------");
            if (userInfo.UserName!=null)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine(userInfo.UserName);
                Console.WriteLine("-------------------------------------------------");
            }
            else
            {                
                Console.WriteLine("请输入用户名(如test1,test2，请勿输入纯数字！)：");
                userInfo.UserName = Console.ReadLine();
            }
            Console.WriteLine("请输入密码(请勿输入个人常用密码)：");
            Console.WriteLine("-------------------------------------------------");
            userInfo.Password= Console.ReadLine();
            Console.WriteLine("-------------------------------------------------");
            if (userInfo.Password!=null)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("-------------------------------------------------");
            }
            else
            {              
                Console.WriteLine("请输入密码(请勿输入个人常用密码)：");
                Console.WriteLine("-------------------------------------------------");
                userInfo.UserName = Console.ReadLine();
                Console.WriteLine("-------------------------------------------------");
            }
            Console.WriteLine("请输入小组编号(不知道的可以去问你们领导)：");
            Console.WriteLine("-------------------------------------------------");
            userInfo.GroupName= Console.ReadLine();
            Console.WriteLine("-------------------------------------------------");
            if (userInfo.GroupName!=null)
            {
                Console.WriteLine(userInfo.GroupName);
            }
            else
            {
                Console.WriteLine("请输入小组编号(不知道的可以去问你们领导)：");
                Console.WriteLine("-------------------------------------------------");
                userInfo.GroupName = Console.ReadLine();
                Console.WriteLine("-------------------------------------------------");
            }
            Console.WriteLine("请输入的你姓名");
            Console.WriteLine("-------------------------------------------------");
            userInfo.UserRealName = Console.ReadLine();
            Console.WriteLine("-------------------------------------------------");
            if (userInfo.UserRealName!=null)
            {
                Console.WriteLine(userInfo.UserRealName);
            }
            else
            {
                Console.WriteLine("请输入的你姓名");
                Console.WriteLine("-------------------------------------------------");
                userInfo.UserRealName = Console.ReadLine();
                Console.WriteLine("-------------------------------------------------");
            }
            Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")));
            userInfo.UTid= r.Next(100000, 999999);//随机生成一个6位整数
            userInfo.CreateDate = DateTime.Now.ToLocalTime();
            userInfo.deleteflag = 0;
            NetCarInfo netCarInfo = new NetCarModel().GetNetCarMac();
            userInfo.UserComputerMac = netCarInfo.NetCarMac;
            return userInfo;
        }
        /// <summary>
        /// 登录的提示页面
        /// </summary>
        /// <returns>返回键盘输入的用户名密码对象</returns>
        public UserInfo LoginUserInfo(UserInfo userInfo)
        {
            
            Console.WriteLine("请输入用户名：");
            userInfo.UserName = Console.ReadLine();
            Console.WriteLine("====================");
            Console.WriteLine("请输入密码：");
            userInfo.Password = Console.ReadLine(); 
            Console.WriteLine("====================");
            return userInfo;

        }

    }
    public class ProjectMenuListInfo
    {
        /// <summary>
        /// 项目列表打印
        /// </summary>
        /// <returns></returns>
        public int Projectmenu(List<ProjectList> promenulist)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("项目连接：");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("项目编号\t\t\t  项目名称");
            foreach (ProjectList menuitem in promenulist)
            {
                if (menuitem.ftid!=0)
                {
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine(menuitem.ftid+" | " + "\t\t\t" + " | " + menuitem.ProjectName);
                    
                }
                
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("系统功能：");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("99996  \t\t加载已连接项目");
            Console.WriteLine("99997  \t\t重置登录状态");
            Console.WriteLine("99998  \t\t重置初始化状态");
            Console.WriteLine("99999  \t\t退出项目(删除挂载磁盘)");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("注意：已经连接过的项目请勿重复输入，老是重复输入到时候软件出了BUG我也难搞");
            Console.WriteLine("----------------------------------------------");
            Console.Write("输入项目编号连接你的项目或选择功能(输入完成按回车)：");
            int code= int.Parse(Console.ReadLine());
            return code;
        }
        /// <summary>
        /// 删除项目的菜单
        /// </summary>
        /// <returns></returns>
        public string UnProject()
        {
            Console.WriteLine("注意：断开项目的盘符如何查看\n" +
                "直接打开我的电脑'我的电脑'，'网络位置'即可看到挂载项目的盘符");
            Console.Write("输入你要断开项目的盘符：");
            string unDisk = Console.ReadLine();
            return unDisk;
        }
        /// <summary>
        /// 项目菜单选择，根据个人权限生成的项目目录
        /// </summary>
        /// <param name="redisdb">reidis数据库连接对象</param>
        /// <param name="sqlConnection">sql数据库连接对象</param>
        /// <param name="loginUsername">用户登录名</param>
        public void ChooseProjecMenu(IDatabase redisdb, SqlConnection sqlConnection, string loginUsername)
        {
            CMDScript cmdScript = new CMDScript();
            //ProjectMenuListInfo projectMenuList = new ProjectMenuListInfo();
            FunctionProject functionProject = new FunctionProject();
            FunctionReset functionReset = new FunctionReset();
            var list = functionProject.GenerateProjectList(sqlConnection, redisdb, loginUsername);
            while (true)
            {
                
                int projectcode = this.Projectmenu(list);
                if (projectcode == 00000)
                {
                    break;
                }
                else if (projectcode == 99996) //加载已连接项目
                {
                    functionProject.AutoMountNetDisk(redisdb);
                }
                else if (projectcode == 99997) //重置登录状态
                {
                    functionReset.ResetLoginStatus(redisdb);
                    Console.WriteLine("重置登录状态完成，请重新登录，按任意键退出程序");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else if (projectcode == 99998) //重置初始化状态
                {
                    functionReset.ResetInitstatus(redisdb, sqlConnection, loginUsername);
                    functionReset.ResetLoginStatus(redisdb);
                    Console.WriteLine("重置初始化设置完成，请重新登录，按任意键退出程序");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else if (projectcode == 99999) //退出项目
                {
                    string disk = this.UnProject();
                    string unmountshell = cmdScript.UnMountNetDiskScript(disk);
                    cmdScript.RunCMDscript(unmountshell);
                }
                else
                {
                    Console.Clear();
                    string remotePath = functionProject.GenerateProjectPath(sqlConnection,projectcode,redisdb);//生成路径
                    if (remotePath!=null)
                    {
                        //挂载网络盘
                        string mountshell = cmdScript.MountNetDiskScript(remotePath);
                        cmdScript.RunCMDscript(mountshell);
                    }
                    else if (remotePath==null)
                    {
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("项目编号输入错误或重复，无法正常挂载项目网络路径，请重新输入项目编号");
                        Console.WriteLine("---------------------------------------------------------------------------");
                    }
                }


            }
        }
    }
    public class MenuAction
    {
        public void ChooseFunction(IDatabase redisdb,SqlConnection sqlConnection)
        {
            ProjectMenuListInfo projectMenuList = new ProjectMenuListInfo();
            FunctionCheck check = new FunctionCheck();
            UserMenu userMenu = new UserMenu();
            UserInfo userInfo = new UserInfo();
            string checkresult= check.InitCheck(redisdb);
            ClineMuemModel clineMuem = new ClineMuemModel();
            if (String.Equals(checkresult,"inited"))
            {
                #region 这里要检查是否为第一次登录的状态
                string loginUsername = check.LoginStatusCheck(redisdb);
                if (loginUsername != null)
                {
                    Console.WriteLine();
                    projectMenuList.ChooseProjecMenu(redisdb, sqlConnection, loginUsername);
                    
                }
                else if (loginUsername == null)
                {
                    clineMuem.FunctionMenu(userInfo, sqlConnection, redisdb, userMenu, projectMenuList);
                }

                #endregion
                //int funcCode = clineMuem.Stratmuem();
                //if (funcCode == 1)
                //{
                    
                //    CMDScript cmdScript = new CMDScript();
                //    UserInfo loguserInfo = userMenu.LoginUserInfo(userInfo);
                //    FunctionUserLogin userLogin = new FunctionUserLogin();
                //    int logincode= userLogin.UserLogin(sqlConnection, loguserInfo);
                //    if (logincode==1)
                //    {
                //        Console.Clear();
                //        Console.WriteLine("用户名密码正确，登录成功");
                //        ProjectMenuListInfo projectMenuList = new ProjectMenuListInfo();
                //        FunctionProject functionProject = new FunctionProject();
                //        var list= functionProject.GenerateProjectList(sqlConnection, redisdb, loguserInfo.UserName);
                //        while (true)
                //        {
                //            int projectcode = projectMenuList.Projectmenu(list);
                //            if (projectcode==00000)
                //            {
                //                break;
                //            }
                //            else if (projectcode == 99999)
                //            {
                //                string disk = projectMenuList.UnProject();
                //                string unmountshell = cmdScript.UnMountNetDiskScript(disk);
                //                cmdScript.RunCMDscript(unmountshell);
                //            }
                //            else
                //            {
                //                Console.Clear();
                //                string remotePath = functionProject.GenerateProjectPath(sqlConnection, projectcode);
                //                string mountshell = cmdScript.MountNetDiskScript(remotePath);
                //                cmdScript.RunCMDscript(mountshell);
                //            }
                           

                //        }

                //    }
                //    else if (logincode == 0)
                //    {
                //        Console.Clear();
                //        Console.WriteLine("用户名密码错误，登录失败，请重新登录");
                //        this.ChooseFunction(redisdb, sqlConnection);
                //    }
                //}
                //else if (funcCode == 0)
                //{
                //    Environment.Exit(0);
                //}
            }
            else //检查初始化状态，当不是初始化的时候代码跑这里
            {
                Console.WriteLine("--------------------------------------");
                userMenu.NoiceInfo();
                UserInfo inituserInfo = userMenu.Init(userInfo);               
                FunctionUserInit userInit = new FunctionUserInit();
                userInit.UserInit(sqlConnection, redisdb, inituserInfo);
                clineMuem.FunctionMenu(userInfo, sqlConnection, redisdb, userMenu, projectMenuList);
            }
            
        }
    }


}
