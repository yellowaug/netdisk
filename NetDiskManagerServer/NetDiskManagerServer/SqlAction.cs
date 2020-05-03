using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NetDiskManagerServer
{
    public class Conndetail
    {
        public string ServerHost { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string DBName { get; set; }

    }
    public class ProjectInfo
    {
        public string ProjectName { get; set; }
        public int ProjectCode { get; set; }
    }
    public class GroupNameInfo
    {
        public string GroupName { get; set; }
        public string UserName { get; set; }
    }
    public class SqlAction
    {
        /// <summary>
        /// 连接SQL数据库的方法
        /// </summary>
        /// <param name="conndetail">传入连接信息：用户名，密码，数据库</param>
        public SqlConnection ConneSql(Conndetail conndetail)
        {
            //拼接连接字符串
            string connstr = String.Format($"Server={conndetail.ServerHost}; Database={conndetail.DBName}; uid={conndetail.DbUser}; pwd={conndetail.DbPassword};");
            SqlConnection connection = new SqlConnection(connstr);
            try
            {
                if (connection.State==ConnectionState.Closed)
                {
                    connection.Open();
                }
                Console.WriteLine("数据库连接成功");
               
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库连接异常！！！！");
                Console.WriteLine(e.Message);               
            }
            return connection;
        }
        /// <summary>
        /// 将文件夹名称插入数据库的方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="folderinfolist"></param>
        public void FolderInfoInser(SqlConnection connection,List<Folderinfo> folderinfolist)
        {

            int n = 10000;
            foreach (var folderitem in folderinfolist)
            {
                n++;//做ftid的数字标记
                //string cmdconten = String.Format($"insert into FolderTable (netpath,foldername,ftid,createdate) values({folderitem.NetPath},{folderitem.FolderName},{n},{folderitem.Createtime})");
                string cmdconten = String.Format($"insert into FolderTable (netpath,folderName,ftid,CreateDate) values('{folderitem.NetPath}','{folderitem.FolderName}',{n},'{folderitem.Createtime}')");
                using (SqlCommand insertcmd = new SqlCommand(cmdconten, connection))
                {
                    int result = insertcmd.ExecuteNonQuery();
                    Console.WriteLine("受影响行数"+result);
                }
            } 
            connection.Close();          
        }
        /// <summary>
        /// 向数据库插入单条数据
        /// </summary>
        /// <param name="connection">数据库连接标识符</param>
        /// <param name="folderinfo">要插入的字段信息</param>
        public void FolderInfoInser(SqlConnection connection, Folderinfo folderinfo)
        {

            Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) );
            int num = r.Next(10000, 99999);//随机生成一个5位整数
            Console.WriteLine("num 的值是：" + num);
            string cmdconten = String.Format($"insert into FolderTable (netpath,folderName,ftid,CreateDate,ProjectName) values('{folderinfo.NetPath}','{folderinfo.FolderName}',{num},'{folderinfo.Createtime}','{folderinfo.ProjectName}')");
            using (SqlCommand insertcmd = new SqlCommand(cmdconten, connection))
            {
                int result = insertcmd.ExecuteNonQuery();
                Console.WriteLine("受影响行数" + result+"数据写入数据库成功");
            }
            connection.Close();
        }
        /// <summary>
        /// 数据库查询功能模板
        /// </summary>
        /// <param name="connection">连接对象实体</param>
        /// <param name="sqlCmd">查询语句</param>
        /// <returns>查询内存对象</returns>
        public DataTable SQLSelect(SqlConnection connection,string sqlCmd)
        {
            //List<ProjectInfo> projectinfoLists = new List<ProjectInfo>();
            //string sqlStr = "select Foldername,Ftid from foldertable";
            DataTable dt = null;
            using (SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt = new DataTable();
                    dt.Load(dr);
                }
                return dt;
                //if (dt != null)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        ProjectInfo project = new ProjectInfo();
                //        for (int j = 0; j < dt.Columns.Count; j++)
                //        {
                //            if (j % 2 == 0)
                //            {
                //                project.ProjectName = dt.Rows[i][j].ToString();
                //                //Console.Write(projectmenu.FolderName);
                //                //Console.Write("\t");
                //            }
                //            else if (j % 2 == 1)
                //            {
                //                project.ProjectCode = int.Parse(dt.Rows[i][j].ToString());
                //                //Console.Write(projectmenu.ftid);
                //            }
                //        }
                //        projectinfoLists.Add(project);
                       
                //    }
                //}
            }
           
        }
        /// <summary>
        /// 查询项目的数据库语句
        /// </summary>
        /// <returns>sql语句</returns>
        public string SelectProject()
        {
            string sqlStr = "select ProjectName,Ftid from foldertable";
            return sqlStr;
        }
        /// <summary>
        /// 查询分组的数据库语句
        /// </summary>
        /// <returns>sql语句</returns>
        public string SelectGroup()
        {
            string sqlStr = "select GroupName,Username from Usertable where deleteflag=0";
            return sqlStr;
        }
    }
}
