using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetdiskManager
{
    public class Shell
    {
        public string ServerHost { get; set; }
        public string DiskPath { get; set; }
        public string FolderPath { get; set; }
        public string ShellMountNetDisk { get; set; }
        public string ShellUnmountNetdisk { get; set; }
    }
    public class ConsonlenList
    {
        /// <summary>
        /// 配置连接服务器网盘的信息以及配置挂载网盘的语句，删除网盘的语句
        /// </summary>
        /// <returns></returns>
        public Shell PWSComand()
        {
            Shell shell = new Shell();
            shell.ServerHost = "10.12.2.19";
            shell.DiskPath = "Z";
            shell.FolderPath = "test";
            shell.ShellMountNetDisk = "New-PSDrive";
            shell.ShellUnmountNetdisk = "Remove-PSDrive";
            return shell;
        }
        
    }

}
