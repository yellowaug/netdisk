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
    /// 获取本地网卡信息,并判断网卡是物理网卡还是wifi
    /// </summary>
    public class NetCarModel
    {
        public NetCarInfo GetNetCarMac()
        {
            NetCarInfo MacInfo = new NetCarInfo();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (String.Equals(adapter.NetworkInterfaceType.ToString(),"Ethernet") && String.Equals(adapter.OperationalStatus.ToString(),"Up"))
                {
                    if (adapter.GetPhysicalAddress().ToString() !=null)
                    {
                        MacInfo.NetCarMac = adapter.GetPhysicalAddress().ToString();
                        break;
                    }
                    else
                    {
                        MacInfo.NetCarMac = adapter.Id;
                        break;
                    }
                }
                else if (String.Equals(adapter.NetworkInterfaceType.ToString(), "Wireless80211") && String.Equals(adapter.OperationalStatus.ToString(), "Up"))
                {
                    MacInfo.NetCarMac = adapter.GetPhysicalAddress().ToString();
                    if (MacInfo.NetCarMac==null)
                    {
                        MacInfo.NetCarMac = adapter.Id;
                        break;
                    }
                }
                //else 
                //{
                //    break;
                //}
                
            }
            //Console.WriteLine(MacInfo.NetCarMac);
            return MacInfo;
        }
    }
}
