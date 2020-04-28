using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDiskManagerServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //CollectInfo collectInfo = new CollectInfo();
            //List<Folderinfo>folderlist= collectInfo.CheckFolder(@"E:\Users");
            Conndetail conndetail = new Conndetail();
            conndetail.ServerHost = "10.12.2.61";
            conndetail.DBName = "TestDemo";
            conndetail.DbUser = "sa";
            conndetail.DbPassword = "sa";
            SqlAction sqlAction = new SqlAction();
            var connet = sqlAction.ConneSql(conndetail);
            //sqlAction.FolderInfoInser(connet, folderlist);
            CreateFolder createFolder = new CreateFolder();
            var createinfo=createFolder.CreateAction(@"e:\", "Test11111");
            sqlAction.FolderInfoInser(connet, createinfo);
            Console.ReadKey();
        }
    }
}
