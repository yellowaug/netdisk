using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetDiskManagerServer
{
    public class Folderinfo
    {
        public string FolderName { get; set; }
        public string NetPath { get; set; }
        public DateTime Createtime { get; set; }        
    }
    public class CollectInfo
    {
        /// <summary>
        /// 收集主文件夹目录下子文件夹的信息
        /// </summary>
        /// <param name="folderPath">要查询文件的路径</param>
        /// <returns>子文件夹名称列表</returns>
        public List<Folderinfo> CheckFolder(string folderPath)
        {
            List<Folderinfo> folderinfolist = new List<Folderinfo>();
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            foreach (var folder in directoryInfo.GetDirectories())
            {
                Folderinfo folderinfo = new Folderinfo();
                folderinfo.FolderName = folder.Name;
                folderinfo.NetPath = "10.12.2.19";
                folderinfo.Createtime = folder.CreationTime;
                folderinfolist.Add(folderinfo);
                Console.WriteLine("当前文件夹下的子文件夹：{0}", folder.FullName);
                Console.WriteLine(folder.CreationTime);

            }
            return folderinfolist;
        }
    }
}
