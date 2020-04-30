using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NetDiskManagerServer
{
   
    public class Connection
    {
        public Conndetail ConnectionDb()
        {
            Conndetail conndetail = new Conndetail();
            conndetail.ServerHost = "10.12.2.61";
            //conndetail.ServerHost = "127.0.0.1";
            conndetail.DBName = "TestDemo";
            conndetail.DbUser = "sa";
            conndetail.DbPassword = "sa";
            return conndetail;
        }
        public IDatabase ConnectionRedis(Conndetail conndetail)
        {
            RedisModel redis = new RedisModel();
            IDatabase db= redis.connectionRedis(conndetail);
            return db;
        }
        public SqlConnection ConnectionSQL(Conndetail conndetail)
        {
            SqlAction sqlAction = new SqlAction();
            SqlConnection connet = sqlAction.ConneSql(conndetail);
            return connet;
        }

    }
    /// <summary>
    /// 权限功能类
    /// </summary>
    public class FunctionAuthority: RedisModel
    {
        //static RedisModel redis = new RedisModel();
        /// <summary>
        /// 项目权限配置
        /// </summary>
        /// <param name="inputProjecName"></param>
        /// <param name="inputlistValue"></param>
        /// <param name="db"></param>
        public void AuthorityAssign(string inputProjecName, string inputlistValue, IDatabase db)
        {            
            this.SetList(inputProjecName, inputlistValue, db);
        }
        public void AuthorityAssignresult(string inputProjecName, IDatabase db)
        {
            List<string> result = this.GetList(inputProjecName, db);
            Console.WriteLine("项目编号\t\t可访问小组编号");
            foreach (var item in result)
            {
                Console.WriteLine($"{inputProjecName}\t\t\t{item}");
            }
        }
    }
    /// <summary>
    /// SQL功能类
    /// </summary>
    public class FunctionSQL:SqlAction
    {
        /// <summary>
        /// 查询项目列表的功能
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <returns>项目列表</returns>
        public List<ProjectInfo> SearchProject(SqlConnection connection)
        {

            DataTable dt = this.SQLSelect(connection, this.SelectProject());
            List<ProjectInfo> projectinfoLists = new List<ProjectInfo>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProjectInfo project = new ProjectInfo();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j % 2 == 0)
                        {
                            project.ProjectName = dt.Rows[i][j].ToString();
                            //Console.Write(project.ProjectName);
                            //Console.Write("\t");
                        }
                        else if (j % 2 == 1)
                        {
                            project.ProjectCode = int.Parse(dt.Rows[i][j].ToString());
                            //Console.Write(project.ProjectCode);
                        }
                    }
                    //Console.WriteLine();
                    projectinfoLists.Add(project);
                }
            }
            return projectinfoLists;
        }
        public List<GroupNameInfo> SearchGroupName(SqlConnection connection)
        {
            DataTable dt = this.SQLSelect(connection, this.SelectGroup());
            List<GroupNameInfo> GroupNameInfo = new List<GroupNameInfo>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GroupNameInfo group = new GroupNameInfo();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j % 2 == 0)
                        {
                            group.GroupName = dt.Rows[i][j].ToString();
                            //Console.Write(group.GroupName);
                            //Console.Write("\t");
                        }
                        else if (j % 2 == 1)
                        {
                            group.UserName = dt.Rows[i][j].ToString();
                            //Console.Write(group.UserName);
                        }
                    }
                    //Console.WriteLine();
                    GroupNameInfo.Add(group);
                }
            }
            return GroupNameInfo;
        }
    }
    /// <summary>
    /// 项目创建类
    /// </summary>
    public class FunctionFolder:SqlAction
    {
        /// <summary>
        /// 创建项目文件夹
        /// </summary>
        /// <param name="diskpath">本地路径磁盘名称</param>
        /// <param name="proName">项目名称</param>
        /// <param name="connet">数据库连接对象</param>
        public void CreateProject(string diskpath,string proName,SqlConnection connet)
        {
            Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")));
            int num = r.Next(100000, 999999);//随机生成一个6位整数
            string localFolderName = String.Format($"{proName}{num}");
            CreateFolder createFolder = new CreateFolder();
            var createinfo = createFolder.CreateAction(diskpath,proName, localFolderName);
            this.FolderInfoInser(connet, createinfo);
        }
    }
}
