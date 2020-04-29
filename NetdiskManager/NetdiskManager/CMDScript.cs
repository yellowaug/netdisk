using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetdiskManager
{
    public class CMDScript
    {
        /// <summary>
        /// CMD命令调用方法
        /// </summary>
        /// <param name="strInput">待执行的CMD命令</param>
        public void RunCMDscript(string strInput)
        {
            //Console.WriteLine("请输入要执行的命令:");
            //string strInput = Console.ReadLine();
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;

            p.StartInfo.Arguments = @"/c" + strInput;
            //启动程序
            p.Start();

            //向cmd窗口发送输入信息
            //p.StandardInput.WriteLine(strInput + "&exit");

            
            p.StandardInput.AutoFlush = true;

            //获取输出信息
            string strOuput = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            Console.WriteLine(strOuput);
        }  
        /// <summary>
        /// 挂载网络磁盘命令
        /// </summary>
        /// <returns>返回挂载脚本</returns>
        public string MountNetDiskScript(string diskname,string remotePath)
        {
            Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")));
            int rannum = r.Next(72, 90);
            char path = (char)rannum;

            string mountscript = String.Format($@"net use {path}: {remotePath}");
            return mountscript;
        }
        /// <summary>
        /// 卸载网络磁盘的方法
        /// </summary>
        /// <param name="diskname">需要卸载的盘符</param>
        /// <returns></returns>
        public string UnMountNetDiskScript(string diskname)
        {
            string unmunt = String.Format($@"net use {diskname}: /del");
            return unmunt;
        }

    }
}
