using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetdiskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //TopMuem topMuem = new TopMuem();
            //topMuem.Stratmuem();
            ConsonlenList consonlenList = new ConsonlenList();
            Shell shell = consonlenList.PWSComand();
            //Console.WriteLine(shell.ShellMountNetDisk);
            //Console.WriteLine(shell.ShellUnmountNetdisk);
            InvokePWshell invokePWshell = new InvokePWshell();
            invokePWshell.MountDiskShell();
            //invokePWshell.UnmountDiskShell();
            Console.ReadKey();
        }
    }
}
