using System;
using System.Management;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using CryptoSysPKI;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.Devices;
using System.Timers;
using Microsoft.VisualBasic;

namespace VAPOR
{
    class GetInfo
    {
       
            static void Main(string[] args) 
            {
                // Get host name
                string strHostName = Dns.GetHostName();
                string WinComputerName = System.Environment.MachineName.ToString();

                // Get PC Uptime from Uptime class.
                System.TimeSpan Uptime = Uptimes.GetUptime();


                foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    string Date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    string PCName = WinComputerName.ToLower();

                    if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {

                        foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {

                            Console.WriteLine(Date + "," + ip.Address.ToString() + "," + netInterface.GetPhysicalAddress().ToString() + "," + WinComputerName + "," + strHostName + "," + HardDrive.GetHardDriveInfo().TrimStart() + "," + new ComputerInfo().OSFullName + "," + Uptime);
                        }
                        }
                    }

                }

        
            }
       
    }
}