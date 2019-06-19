using System;
using System.Management;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using CryptoSysPKI;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace PCNetworkGrabber
{
    class GetInfo
    {
        static void getHardDriveInfo()
        {
            ArrayList hdCollection = new ArrayList();
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.Model = wmi_HD["Model"].ToString();
                hd.Type = wmi_HD["InterfaceType"].ToString();
                hdCollection.Add(hd);
            }

            searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                HardDrive hd = (HardDrive)hdCollection[i];

                // get the hardware serial no.
                if (wmi_HD["SerialNumber"] == null)
                    hd.SerialNo = "None";
                else
                    hd.SerialNo = wmi_HD["SerialNumber"].ToString();

                ++i;
            }
            static void Main(string[] args)
            {
                // Get host name
                string strHostName = Dns.GetHostName();
                string WinComputerName = System.Environment.MachineName.ToString();


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

                                Console.WriteLine(Date + "," + ip.Address.ToString() + "," + netInterface.GetPhysicalAddress().ToString() + "," + WinComputerName + "," + strHostName);
                            }
                        }
                    }

                }
                foreach (HardDrive hd in hdCollection)
                {
                    Console.WriteLine("Model\t\t: " + hd.Model);
                    Console.WriteLine("Type\t\t: " + hd.Type);
                    Console.WriteLine("Serial No.\t: " + hd.SerialNo);
                    Console.WriteLine();
                }


            }
        }
    }
}