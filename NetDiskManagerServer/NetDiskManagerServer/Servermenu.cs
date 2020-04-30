using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDiskManagerServer
{
    public class Servermenu
    {
        public void Startmenu()
        {
            Console.WriteLine("==========================");
            Console.WriteLine();
            Console.WriteLine("1.\t新建项目");
            Console.WriteLine();
            Console.WriteLine("2.\t项目目录导入(单个目录导入)");
            Console.WriteLine();
            Console.WriteLine("3.\t项目分配");
            Console.WriteLine();
            Console.WriteLine("0.\t退出");
            Console.WriteLine();
            Console.WriteLine("==========================");
        }

    }
   
    public class MenuAction
    {
        static Servermenu servermenu = new Servermenu();
        public void StartAction()
        {
            Connection connection = new Connection();
            FunctionSQL functionSQL = new FunctionSQL();
            FunctionAuthority functionAuthority = new FunctionAuthority();
            Conndetail conndetail = connection.ConnectionDb();
            servermenu.Startmenu();
            Console.Write("请选择功能：");
            int code= int.Parse(Console.ReadLine());
            if (code == 1)
            {
                Console.Clear();
            }
            else if (code==2)
            {
                Console.Clear();
            }
            else if (code==3)
            {
                Console.Clear();                              
                var connSql= connection.ConnectionSQL(conndetail);
                var connRedis = connection.ConnectionRedis(conndetail);
                List<ProjectInfo> projects= functionSQL.SearchProject(connSql);
                List<GroupNameInfo> groupNames = functionSQL.SearchGroupName(connSql);


                Console.WriteLine("项目编号 \t\t\t 项目名称");
                Console.WriteLine("---------------------------------------------");
                for (int i = 0; i < projects.Count; i++)
                {
                    Console.WriteLine($"|{projects[i].ProjectCode}| \t\t\t |{projects[i].ProjectName}|");
                }
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("小组编号 \t\t\t 小组人员");
                Console.WriteLine("---------------------------------------------");
                foreach (var useritem in groupNames)
                {
                    Console.WriteLine($"|{useritem.GroupName}| \t\t\t |{useritem.UserName}|");
                }
                Console.WriteLine("---------------------------------------------");
                //functionAuthority.AuthorityAssign()
                Console.WriteLine();
            }
            else if (code==0)
            {
                    
            }
            else
            {
                Console.WriteLine("请输入正确的功能编号选择功能！");
                StartAction();
            }
        }
    }
}
