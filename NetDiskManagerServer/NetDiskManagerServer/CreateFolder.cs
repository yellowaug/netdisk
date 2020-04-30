using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetDiskManagerServer
{
    public class CreateFolder
    {
        /// <summary>
        /// 创建文件夹的方法
        /// </summary>
        /// <param name="path">路径地址 如E:\</param>
        /// <param name="proName">文件夹名称</param>
        public Folderinfo CreateAction(string path,string proName,string localfolderName)
        {
            Folderinfo folderinfo = new Folderinfo();
            folderinfo.FolderName = localfolderName;
            folderinfo.ProjectName = proName;
            folderinfo.NetPath = "10.12.2.19";//插入数据库的IP
            folderinfo.Createtime= DateTime.Now.ToLocalTime();//获取当前时间
            string pathStr = Path.Combine(path, localfolderName);
            Console.WriteLine(pathStr);
            try
            {
                Directory.CreateDirectory(pathStr);
                Console.WriteLine($"文件创建成功：{pathStr}");                
            }
            catch (IOException ioe)
            {
                Console.WriteLine($"文件创建失败,返回的异常是：\n{ioe.Message}");                
            }
            return folderinfo;
        }
    }
}
