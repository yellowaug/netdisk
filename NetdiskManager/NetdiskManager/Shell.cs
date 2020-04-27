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
        public string ShellConsolen { get; set; }
    }
    public class ConsonlenList
    {
        
        public void PWSComand()
        {
            Shell shell = new Shell();
            shell.ServerHost = "10.12.2.19";
            shell.DiskPath = "z";
            shell.FolderPath = "test";
            shell.ShellConsolen = $"New-PSDrive -Name " + shell.DiskPath + " -PSProvider FileSystem -Root '\\\\" + shell.ServerHost + $"\\" + shell.FolderPath + $"'-Persist -Scope Global";
        }
        
    }

}
