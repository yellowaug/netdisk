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
        /// C#调用PWS命令持久化挂载网络硬盘。
        /// </summary>
        public void MountDiskShell()
        {

            ConsonlenList consonlenList = new ConsonlenList();
            Shell shell = consonlenList.PWSComand();
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {

                PowerShellInstance.AddCommand(shell.ShellMountNetDisk)
                    .AddParameter("Name",shell.DiskPath)
                    .AddParameter("PSProvider", "FileSystem")
                    .AddParameter("root",@"\\"+shell.ServerHost+@"\"+shell.FolderPath)
                    .AddParameter("Persist");
                
                Collection<PSObject> psResult = new Collection<PSObject>();
                try
                {
                    psResult = PowerShellInstance.Invoke();
                    Console.WriteLine(psResult.Count);
                    foreach (PSObject outputItem in psResult)
                    {
                        if (outputItem.BaseObject.ToString() == shell.DiskPath)
                        {
                            Console.WriteLine("磁盘挂载成功");
                        };
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
        }
        //这个方法还是有问题,用NER USER Y: /DELETE命令可以解决
        public void UnmountDiskShell()
        {
            ConsonlenList consonlenList = new ConsonlenList();
            Shell shell = consonlenList.PWSComand();
            using (PowerShell Instance = PowerShell.Create())
            {
                Instance.AddCommand("Get-PSDrive").AddParameter("Name",shell.DiskPath);
                    
                Collection<PSObject> psResult = new Collection<PSObject>();
                try
                {
                    psResult=Instance.Invoke();
                    foreach (PSObject outputitem in psResult)
                    {
                        if (outputitem.BaseObject.ToString()==shell.DiskPath)
                        {
                            Instance.AddCommand("Remove-PSDrive").AddParameter("Name", shell.DiskPath);
                            psResult=Instance.Invoke();
                            Console.WriteLine(psResult.Count());
                        }
                    }
                    Console.WriteLine(psResult);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }


            }
        }
    }
}
