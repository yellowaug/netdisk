using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NetdiskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //TopMuem topMuem = new TopMuem();
            //topMuem.Stratmuem();
            //ConsonlenList consonlenList = new ConsonlenList();
            //Shell shell = consonlenList.PWSComand();
            //Console.WriteLine(shell.ShellMountNetDisk);
            //Console.WriteLine(shell.ShellUnmountNetdisk);
            //InvokePWshell invokePWshell = new InvokePWshell();
            //invokePWshell.MountDiskShell();
            //invokePWshell.UnmountDiskShell();
            Conndetail conndetail = new Program().InitConnet();
            //new Program().FunctionUserInitInvoke(conndetail);
            new Program().FunctionUserLoginInvoke(conndetail);
            Console.ReadKey();
        }
        /// <summary>
        /// 数据库连接初始化
        /// </summary>
        /// <returns>返回连接初始化信息对象</returns>
        Conndetail InitConnet()
        {
            Conndetail conndetail = new Conndetail();
            conndetail.ServerHost = "127.0.0.1";
            conndetail.DBName = "TestDemo";
            conndetail.DbUser = "sa";
            conndetail.DbPassword = "sa";

            return conndetail;
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
        void FunctionUserLoginInvoke(Conndetail conndetail)
        {
            SqlAction sqlAction = new SqlAction();
            var connet = sqlAction.ConneSql(conndetail);
            InitUser initUser = new InitUser();
            UserInfo userInfoLogin = initUser.LoginUserInfo();
            string cmd = sqlAction.SelectUser(userInfoLogin.UserName, userInfoLogin.Password);
            var dt= sqlAction.SeletScript(cmd, connet);
            Console.WriteLine(dt);
        }
        
    }
}
