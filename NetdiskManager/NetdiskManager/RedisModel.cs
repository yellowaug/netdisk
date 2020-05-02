using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace NetdiskManager
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
                Console.WriteLine(db.StringGet(key));
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
        /// <summary>
        /// 根据Key值，获取链表所有值
        /// </summary>
        /// <param name="inputProjecName">项目名称，也可以是任意KEY值</param>
        /// <param name="db">Redis数据库连接对象</param>
        /// <returns>查询结果对象</returns>
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
        public void SetList(string inputkey,string inputvalue,IDatabase redisdb)
        {
            try
            {
                long flag= redisdb.ListLeftPush(inputkey, inputvalue);
                if (flag!=0)
                {
                    Console.WriteLine($"{inputvalue}写入redis成功");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            
        }
        /// <summary>
        /// 删除redis key的方法
        /// </summary>
        /// <param name="inputkey">要删除的KEY</param>
        /// <param name="db">redisdb连接对象</param>
        public void DelKey(string inputkey, IDatabase db)
        {
            try
            {
                if (db.KeyDelete(inputkey))
                {
                    Console.WriteLine($"{inputkey}删除成功");
                }
                
            }
            catch (Exception e)
            {

                Console.WriteLine(e); ;
            }
        }
    }
}
