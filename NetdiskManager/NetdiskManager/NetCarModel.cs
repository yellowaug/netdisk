using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace NetdiskManager
{
    public class NetCarInfo
    {
        public string NetCarMac { get; set; }
    }
    /// <summary>
    /// 获取本地网卡信息
    /// </summary>
    public class NetCarModel
    {
        public NetCarInfo GetNetCarMac()
        {
            NetCarInfo MacInfo = new NetCarInfo();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (String.Equals(adapter.NetworkInterfaceType.ToString(),"Ethernet"))
                {
                    MacInfo.NetCarMac = adapter.GetPhysicalAddress().ToString();
                }
                else
                {
                    break;
                }
                
            }
            //Console.WriteLine(MacInfo.NetCarMac);
            return MacInfo;
        }
    }
}
