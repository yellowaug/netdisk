using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace NetdiskManager
{
    public class InvokePWshell
    {
        /// <summary>
        /// C#调用PWS命令的方法模板，可获取结果。
        /// </summary>
        public void MountDiskShell()
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddCommand("Get-PSDrive");
                Collection<PSObject> psResult = new Collection<PSObject>();
                psResult = PowerShellInstance.Invoke();
                foreach (PSObject outputItem in psResult)
                {
                    Console.WriteLine(outputItem.Properties);
                }
            }
        }
    }
}
