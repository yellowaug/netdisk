using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NetdiskManager
{
    /// <summary>
    /// 数据库UserTable表
    /// </summary>
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UTid { get; set; }
        public string GroupName { get; set; }
        public string UserComputerMac { get; set; }
        public int deleteflag { get; set; }
        public string UserRealName { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class Conndetail
    {
        public string ServerHost { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string DBName { get; set; }

    }
    public class ProjectList
    {
        public string ProjectName { get; set; }
        public int ftid { get; set; }
    }
    public class SqlAction:RedisModel
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
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Console.WriteLine("数据库连接正常");
                }
                //Console.WriteLine(connection.State);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }
            return connection;
        }
        /// <summary>
        /// 数据库查询的方法
        /// </summary>
        /// <param name="sqlcommand">查询语句</param>
        /// <param name="connection">连接实例</param>
        /// <returns>返回查询结果的内存对象</returns>
        public DataTable SeletScript(string sqlcommand, SqlConnection connection)
        {
            DataTable dt = null;
            using (SqlCommand cmd = new SqlCommand(sqlcommand, connection))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt = new DataTable();
                    dt.Load(dr);
                }
                return dt;
            }            
        }
        /// <summary>
        /// 初始化用户处插入SQL语句
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="connection">数据库连接对象</param>
        public void UserInsert(UserInfo userInfo, SqlConnection connection)
        {
            string cmdconten = String.Format($"insert into UserTable (Username,GroupName,Password,Utid,CreateDate,UserComputerMac,deleteflag,UserRealName) " +
                $"values('{userInfo.UserName}','{userInfo.GroupName}',{userInfo.Password},'{userInfo.UTid}','{userInfo.CreateDate}','{userInfo.UserComputerMac}','{userInfo.deleteflag}','{userInfo.UserRealName}')");
            using (SqlCommand insertcmd = new SqlCommand(cmdconten, connection))
            {

                int result = insertcmd.ExecuteNonQuery();
                if (result==1)
                {
                    //Console.WriteLine("UserTable数据插入成功");
                    Console.WriteLine("用户输入数据写入成功");
                }

            }
        }
        /// <summary>
        /// 数据库更新功能
        /// </summary>
        /// <param name="sqlSricpt"></param>
        /// <param name="connection"></param>
        public void UpdataSQL(string sqlSricpt, SqlConnection connection)
        {
            using (SqlCommand updatacmd = new SqlCommand(sqlSricpt, connection))
            {

                int result = updatacmd.ExecuteNonQuery();
                if (result == 1)
                {
                    //Console.WriteLine("UserTable数据插入成功");
                    Console.WriteLine("用户更新数据写入成功");
                }
                else
                {
                    Console.WriteLine("用户更新数据失败");
                }

            }
        }
        /// <summary>
        /// 查询项目盘的语句
        /// </summary>
        /// <returns></returns>
        public string SelectDisk()
        {
            string cmd = "select ProjectName,ftid from FolderTable";
            return cmd;
        }
        /// <summary>
        /// 登录时用到的语句
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public string SelectUser(string userName,string passWord)
        {
            string cmd = String.Format($"select * from UserTable where username='{userName}'and password='{passWord}' and deleteflag=0");
            return cmd;
        }
        /// <summary>
        /// 根据项目编号查询项目
        /// </summary>
        /// <param name="code">项目编号</param>
        /// <returns></returns>
        public string SelectProjectCode(int code)
        {
            string cmd = String.Format($"select foldername from FolderTable where ftid={code}");
            return cmd;
        }
        /// <summary>
        /// 根据用户名查询
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string SelectGroupID(string userName)
        {
            string cmd = String.Format($"select groupname from Usertable where username='{userName}'and deleteflag=0");
            return cmd;
        }
        /// <summary>
        /// 更新删除标志的语句
        /// </summary>
        /// <param name="loginUsername">用户名</param>
        /// <returns></returns>
        public string UpdataDeleteFlag(string loginUsername)
        {
            string cmd = String.Format($"update usertable set deleteflag=1 where username='{loginUsername}'");
            return cmd;
                
        }

    }
}
