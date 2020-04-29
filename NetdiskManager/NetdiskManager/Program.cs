using System;
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
            CMDScript script = new CMDScript();
            script.Cmditem();
            #endregion

            //new Program().MenuAction();
            Console.ReadKey();
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
        void MenuAction()
        {
            Conndetail conndetail = new Program().InitConnet();
            TopMuem topMuem = new TopMuem();

            int changecode=topMuem.Stratmuem();
            if (changecode==1)
            {
                int loginCode = FunctionUserLoginInvoke(conndetail);
                if (loginCode==1)
                {
                    Console.WriteLine("登录成功");
                    int code =FunctionprojectLists(conndetail);
                    string path=FunctionprojectPath(conndetail, code);
                    //挂载网盘
                    new InvokePWshell().MountDiskShell(path);
                }
                else 
                {
                    Console.WriteLine("登录失败，重新登录");                    
                }

            }
            if (changecode == 9)
            {
                FunctionUserInitInvoke(conndetail);
            }
        }
        /// <summary>
        /// 调用用户初始化方法
        /// </summary>
        /// <param name="conndetail">sql连接对象</param>
        void FunctionUserInitInvoke(Conndetail conndetail)
        {
            SqlAction sqlAction = new SqlAction();
            var connet = sqlAction.ConneSql(conndetail);
            InitUser initUser = new InitUser();           
            UserInfo userInfo= initUser.Init();
            sqlAction.UserInsert(userInfo, connet);
        }
        /// <summary>
        /// 调用用户登录方法
        /// </summary>
        /// <param name="conndetail">sql连接对象</param>
        int FunctionUserLoginInvoke(Conndetail conndetail)
        {
            SqlAction sqlAction = new SqlAction();
            var connet = sqlAction.ConneSql(conndetail);
            InitUser initUser = new InitUser();
            UserInfo userInfoLogin = initUser.LoginUserInfo();
            string cmd = sqlAction.SelectUser(userInfoLogin.UserName, userInfoLogin.Password);
            var dt= sqlAction.SeletScript(cmd, connet);
            if (dt!=null)
            {
                Console.WriteLine("用户名密码正确，登录成功");
                return dt.Rows.Count;
            }
            else
            {
                Console.WriteLine("用户名或密码错误，请重新登录");
                return dt.Rows.Count;
            }
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
        }
        /// <summary>
        /// 调用项目列表读取功能，查询数据库，显示项目列表
        /// </summary>
        /// <param name="conndetail"></param>
        /// <returns>返回用户输入的项目编号</returns>
        int FunctionprojectLists(Conndetail conndetail)
        {
            SqlAction sqlAction = new SqlAction();
            ProjectMenuListInfo promenu = new ProjectMenuListInfo();
            List<ProjectList> projectmenuLists = new List<ProjectList>();
            var connet = sqlAction.ConneSql(conndetail);
            string cmd = sqlAction.SelectDisk();
            var dt = sqlAction.SeletScript(cmd, connet);
            if (dt!=null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProjectList projectmenu = new ProjectList();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j%2==0)
                        {
                            projectmenu.FolderName = dt.Rows[i][j].ToString();
                            //Console.Write(projectmenu.FolderName);
                            //Console.Write("\t");
                        }                        
                        else if (j%2==1)
                        {
                            projectmenu.ftid = int.Parse(dt.Rows[i][j].ToString());
                            //Console.Write(projectmenu.ftid);
                        }
                        
                        //Console.Write(dt.Rows[i][j]);
                        //Console.Write("\t");
                        //Console.WriteLine(j%2);
                    }
                    projectmenuLists.Add(projectmenu);
                    //Console.WriteLine();
                }                                
            }
            return promenu.Projectmenu(projectmenuLists);
        }
        string FunctionprojectPath(Conndetail conndetail,int projectcode)
        {
            SqlAction sqlAction = new SqlAction();
            var connet = sqlAction.ConneSql(conndetail);
            string cmd = sqlAction.SelectProjectCode(projectcode);
            var dt = sqlAction.SeletScript(cmd, connet);
            if (dt!=null)
            {
                
                string remotePath = Path.Combine("10.12.2.19", dt.Rows[0][0].ToString());
                Console.WriteLine(@"查询到的项目路径为{0}",remotePath);
                return dt.Rows[0][0].ToString();
            }
            else
            {
                Console.WriteLine("项目编号输入错误");
                return null;
            }
        }
        
    }
}
