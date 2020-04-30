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
            #region 测试代码
            //CollectInfo collectInfo = new CollectInfo();
            //List<Folderinfo>folderlist= collectInfo.CheckFolder(@"E:\Users");
            //Conndetail conndetail = new Conndetail();
            //conndetail.ServerHost = "10.12.2.61";
            //conndetail.ServerHost = "127.0.0.1";
            //conndetail.DBName = "TestDemo";
            //conndetail.DbUser = "sa";
            //conndetail.DbPassword = "sa";
            //SqlAction sqlAction = new SqlAction();
            //var connet = sqlAction.ConneSql(conndetail);
            //var list = sqlAction.SearchProject(connet);
            //foreach (var listitem in list)
            //{
            //    Console.WriteLine(listitem.ProjectName);
            //}
            //sqlAction.FolderInfoInser(connet, folderlist);
            //CreateFolder createFolder = new CreateFolder();
            //var createinfo=createFolder.CreateAction(@"e:\", "Test11111");
            //sqlAction.FolderInfoInser(connet, createinfo);
            //RedisModel redis = new RedisModel();
            //var conn = redis.connectionRedis(conndetail);
            //string[] userlist = {"test1","test2","test3","test4"};
            //foreach (var item in userlist)
            //{
            //    redis.SetAuthorityList("test111", item, conn);
            //}           
            //var test= redis.GetList("test111", conn);
            //foreach (var item in test)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion
            MenuAction menuAction = new MenuAction();
            menuAction.StartAction();
            Console.ReadKey();
        }
    }
}
