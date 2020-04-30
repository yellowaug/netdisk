using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDiskManagerServer
{
    public class RedisModel
    {
        /// <summary>
        /// 连接Redis服务器
        /// </summary>
        /// <param name="conndetail">服务器地址信息对象</param>
        /// <returns></returns>
        public IDatabase connectionRedis(Conndetail conndetail)
        {
            ConnectionMultiplexer redisClient = ConnectionMultiplexer.Connect(conndetail.ServerHost);
            IDatabase db = redisClient.GetDatabase();
            return db;
        }
        /// <summary>
        /// 设置KEY-VALUE方法
        /// </summary>
        /// <param name="inpuetvalue">输入的值 value</param>
        /// <param name="db">数据库连接对象</param>
        /// <param name="inputKey">输入的KEY</param>
        public void SetLoginStatus(string inpuetvalue, IDatabase db, string inputKey)
        {
            string key = inputKey;
            string value = inpuetvalue;
            if (db.StringSet(key, value))
            {
                Console.WriteLine("数据插入Redis成功");
                //Console.WriteLine(db.StringGet(key));
            }
            else
            {
                Console.WriteLine("数据插入失败");
                throw new Exception();
            }

        }
        /// <summary>
        /// 根据Key查询数据
        /// </summary>
        /// <param name="inputKey">KEY的值</param>
        /// <param name="db">数据库连接对象</param>
        /// <returns></returns>
        public string GetLoginStatus(string inputKey, IDatabase db)
        {
            return db.StringGet(inputKey);
        }
        public void SetList(string inputProjecName,string inputlistValue, IDatabase db)
        {
            long a= db.ListLeftPush(inputProjecName, inputlistValue);
            if (a!=0)
            {
                Console.WriteLine("数据插入成功");
            }
        }
        public List<string> GetList(string inputProjecName, IDatabase db)
        {
            List<string> resultList = new List<string>();
            //var a= db.ListLeftPop(inputProjecName);
            var result = db.ListRange(inputProjecName);
            foreach (var item in result)
            {
                resultList.Add(item);
            }
            return resultList;
        }

    }
}
