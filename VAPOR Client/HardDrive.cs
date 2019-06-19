using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace VAPOR
{
    /// <summary>
    /// This "HardDrive" class will obtain the Serial ID of the the user's HardDrive using a GetHardDriveInfo() method.
    /// </summary>
    public class HardDrive
    {
        private string serialNo = null;
        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }
        public static String GetHardDriveInfo()
        {
            ArrayList hdCollection = new ArrayList();
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
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
            foreach (HardDrive hd in hdCollection)
            {
                return hd.SerialNo.ToString();

            }
            return "null";
        }
    }
}

