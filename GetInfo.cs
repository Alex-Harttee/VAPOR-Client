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
using System.IO;

namespace VAPOR
{
    class GetInfo
    {
       
            static void Main(string[] args) 
            {
                string Date1 = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy UTC HH;mm;ss ");
                string pathString = @"C:\Program Files (x86)\VAPOR Client\Logs";
                string fileName = Date1 + "logs.csv";
                Directory.CreateDirectory(pathString);
                pathString = System.IO.Path.Combine(pathString, fileName);

            





                // Get host name
                string strHostName = Dns.GetHostName();
                string WinComputerName = System.Environment.MachineName.ToString();

                // Get PC Uptime from Uptime class.
                System.TimeSpan Uptime = Uptimes.GetUptime();

                // Get Public IP from PublicIP class.
                string publicIP = PublicIP.getPublicIP();


                foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    string Date = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy");
                    string PCName = WinComputerName.ToLower();

                    if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {

                        foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                if (!System.IO.File.Exists(pathString))
                                {
                                    Console.WriteLine("Creating file");
                                    System.IO.FileStream fs = System.IO.File.Create(pathString);
                                    fs.Close(); 
                                    {
                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathString))
                                        {
                                            file.WriteLine(Date + "," + ip.Address.ToString() + "," + netInterface.GetPhysicalAddress().ToString() + "," + WinComputerName + "," + strHostName + "," + HardDrive.GetHardDriveInfo().TrimStart() + "," + new ComputerInfo().OSFullName + "," + Uptime);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("File \"{0}\" already exists, appending file", fileName);
                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathString, true))
                                    {
                                        file.WriteLine(Date + "," + ip.Address.ToString() + "," + netInterface.GetPhysicalAddress().ToString() + "," + WinComputerName + "," + strHostName + "," + HardDrive.GetHardDriveInfo().TrimStart() + "," + new ComputerInfo().OSFullName + "," + Uptime);
                                    }
                                }
                            }
                        }
                    }

                }
                
                

        
            }
       
    }
}