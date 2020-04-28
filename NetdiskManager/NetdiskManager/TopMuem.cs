using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetdiskManager
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UTid { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class TopMuem
    {
        public void Stratmuem()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("1:连\t接\t项\t目");
            Console.WriteLine();
            Console.WriteLine("2:退\t出\t项\t目");
            Console.WriteLine();
            Console.WriteLine("0:退\t出\t程\t序");
            Console.WriteLine("=================================");
        }
    }
    public class InitUser
    {
        /// <summary>
        /// 初始化用户信息，用户输入，账号密码小组号写入数据库
        /// </summary>
        /// <returns></returns>
        public UserInfo Init()
        {
            UserInfo userInfo = new UserInfo();
            Console.WriteLine("请输入用户名：");
            userInfo.UserName= Console.ReadLine();
            if (userInfo.UserName!=null)
            {
                Console.WriteLine("====================");
                Console.WriteLine(userInfo.UserName);
                Console.WriteLine("====================");
            }
            else
            {                
                Console.WriteLine("请输入用户名：");
                userInfo.UserName = Console.ReadLine();
            }
            Console.WriteLine("请输入密码：");
            userInfo.Password= Console.ReadLine();
            if (userInfo.Password!=null)
            {
                Console.WriteLine("====================");
                Console.WriteLine("***************");
                Console.WriteLine("====================");
            }
            else
            {
                Console.WriteLine("请输入密码：");
                userInfo.UserName = Console.ReadLine();
            }
            Console.WriteLine("请输入小组编号(可空)");
            userInfo.GroupName= Console.ReadLine();
            if (userInfo.GroupName!=null)
            {
                Console.WriteLine(userInfo.GroupName);
            }
            Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")));
            userInfo.UTid= r.Next(100000, 999999);//随机生成一个6位整数
            userInfo.CreateDate = DateTime.Now.ToLocalTime();
            return userInfo;
        }
        /// <summary>
        /// 登录的提示页面
        /// </summary>
        /// <returns>返回键盘输入的用户名密码对象</returns>
        public UserInfo LoginUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            Console.WriteLine("请输入用户名：");
            userInfo.UserName = Console.ReadLine();
            Console.WriteLine("====================");
            Console.WriteLine("请输入密码：");
            userInfo.Password = Console.ReadLine();
            Console.WriteLine("====================");
            return userInfo;

        }
    }
}
