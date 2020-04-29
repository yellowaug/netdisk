﻿using System;
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
        public void SetLoginStatus(string inpuetvalue, IDatabase db,string inputKey)
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

    }
}