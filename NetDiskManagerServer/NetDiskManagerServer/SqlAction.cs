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
                Console.WriteLine(connection.State);
               
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);               

            }
            return connection;
        }
        /// <summary>
        /// 
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
    }
}
