using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using StackExchange.Redis;


namespace NetdiskManager
{
    public class FunctionCheck
    {
        /// <summary>
        /// 数据库连接初始化
        /// </summary>
        /// <returns>返回连接初始化信息对象</returns>
        //public SqlConnection InitConnet()
        //{
        //    Conndetail conndetail = new Conndetail();
        //    conndetail.ServerHost = "10.12.2.61";
        //    //conndetail.ServerHost = "127.0.0.1";
        //    conndetail.DBName = "TestDemo";
        //    conndetail.DbUser = "sa";
        //    conndetail.DbPassword = "sa";
        //    SqlAction sqlAction = new SqlAction();
        //    SqlConnection connection = sqlAction.ConneSql(conndetail);

        //    return connection;
        //}
        public string InitCheck(IDatabase redisdb)
        {           
            NetCarModel netCarModel = new NetCarModel();
            RedisModel redis = new RedisModel();
            NetCarInfo netCarInfo = netCarModel.GetNetCarMac();
            string rediskey = String.Format($"init{netCarInfo.NetCarMac}");
            string initstatus= redis.GetLoginStatus(rediskey, redisdb);
            return initstatus;
        }
        public string LoginStatusCheck(IDatabase redisdb)
        {
            NetCarModel netCarModel = new NetCarModel();
            RedisModel redis = new RedisModel();
            NetCarInfo netCarInfo = netCarModel.GetNetCarMac();
            string rediskey = String.Format($"loginstatus{netCarInfo.NetCarMac}");
            string loginstatus = redis.GetLoginStatus(rediskey, redisdb);
            return loginstatus;
        }
    }

 
    public class FunctionUserInit : SqlAction
    {
        /// <summary>
        /// 登录用户初始化方法
        /// </summary>
        /// <param name="conndetail">sql连接对象</param>
        public void UserInit(SqlConnection connection, IDatabase redisdb,UserInfo inituserInfo)
        {
            NetCarModel netCarModel = new NetCarModel();
            //InitUser initUser = new InitUser();
            //RedisModel redis = new RedisModel();
            //SqlAction sqlAction = new SqlAction();
            //UserInfo userInfo = initUser.Init(loginUserinfo);
            this.UserInsert(inituserInfo, connection);
            NetCarInfo netCarInfo = netCarModel.GetNetCarMac();
            string rediskey = String.Format($"init{netCarInfo.NetCarMac}");
            this.SetLoginStatus("inited", redisdb, rediskey);
        }
    }
    public class FunctionUserLogin: SqlAction
    {
        /// <summary>
        /// 用户登录方法
        /// </summary>
        /// <param name="conndetail">数据库连接对象</param>
        /// <param name="loginUserinfo">登录用户信息</param>
        /// <returns>登录查询信息</returns>
        public int UserLogin(SqlConnection connection, UserInfo loginUserinfo)
        {
            
            //InitUser initUser = new InitUser();
            //UserInfo userInfoLogin = initUser.LoginUserInfo(loginUserinfo);
            string cmd = this.SelectUser(loginUserinfo.UserName, loginUserinfo.Password);
            var dt = this.SeletScript(cmd, connection);
            return dt.Rows.Count;
            //if (dt.Rows != null)
            //{
            //    Console.WriteLine("用户名密码正确，登录成功");
            //    return dt.Rows.Count;
            //}
            //else
            //{
            //    Console.WriteLine("用户名或密码错误，请重新登录");
            //    return dt.Rows.Count;
            //}
        }
        public void SetUserLoginStatus(IDatabase redisdb,string loginName)
        {
            NetCarModel netCarModel = new NetCarModel();
            RedisModel redis = new RedisModel();
            NetCarInfo netCarInfo = netCarModel.GetNetCarMac();
            string rediskey = String.Format($"loginstatus{netCarInfo.NetCarMac}");
            this.SetLoginStatus(loginName, redisdb, rediskey);
            Console.WriteLine("登录状态设置成功，下次可以不用输入账号密码登录了");
        }

    }
    /// <summary>
    /// 项目列表生成类
    /// 这个类是本程序的核心
    /// </summary>
    public class FunctionProject: SqlAction
    {
        /// <summary>
        /// 项目列表生产方法
        /// 根据MAC地址查询Redis中存的用户名，然后根据用户名的用户ID比对Reids项目权限列表中是否有存在这个ID
        /// 如果存在则生产项目列表
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="redisdb">redis连接对象</param>
        /// <param name="userName">根据MAC查询到的本机用户名</param>
        /// <returns>返回根据权限查询获得的项目列表</returns>
        public List<ProjectList> GenerateProjectList(SqlConnection connection, IDatabase redisdb, string userName)
        {
            try
            {
                List<ProjectList> projectLists = new List<ProjectList>();
                DataTable dtgroupID = this.SeletScript(this.SelectGroupID(userName), connection);
                string groupId = dtgroupID.Rows[0][0].ToString(); //根据用户名查询到的用户ID

                DataTable dtprojectlist = this.SeletScript(this.SelectDisk(), connection);
                for (int i = 0; i < dtprojectlist.Rows.Count; i++)
                {
                    ProjectList projectList = new ProjectList();
                    for (int j = 0; j < dtprojectlist.Columns.Count; j++)
                    {
                        if (j%2==0)
                        {
                            string projectName= dtprojectlist.Rows[i][j].ToString();
                            projectList.ProjectName = projectName;
                            //Console.WriteLine($"{i}////{j}/////{dtprojectlist.Rows[i][j]}");
                        }
                       
                        if (j % 2 != 0)
                        {
                            string projectID = dtprojectlist.Rows[i][j].ToString();
                            //Console.WriteLine(projectID);
                            //Console.WriteLine();
                            List<string> ProjectList = this.GetList(projectID, redisdb);
                            foreach (var projectitem in ProjectList)
                            {
                                if (projectitem.Equals(groupId))
                                {

                                    projectList.ftid = int.Parse(dtprojectlist.Rows[i][j].ToString());
                                    //Console.WriteLine($"{projectList.ProjectName}++++++{projectList.ftid}");
                                }
                            }
                        }
                    }
                    projectLists.Add(projectList);
                }
                return projectLists;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
                                   
        }
        /// <summary>
        /// 根据项目编号生成网盘挂载路径
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="projectcode">项目编码</param>
        /// <returns></returns>
        public string GenerateProjectPath(SqlConnection connection, int projectcode)
        {                      
            string cmd = this.SelectProjectCode(projectcode);
            var dt = this.SeletScript(cmd, connection);
            if (dt != null)
            {
                string remotePath = Path.Combine("127.0.0.1", dt.Rows[0][0].ToString());//远程主机IP
                remotePath = String.Format($@"\\{remotePath}");
                Console.WriteLine(@"查询到的项目路径为{0}", remotePath);
                return remotePath;
            }
            else
            {
                Console.WriteLine("项目编号输入错误");
                return null;
            }
        }
    }
}
