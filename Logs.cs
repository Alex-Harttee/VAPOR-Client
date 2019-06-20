using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace VAPOR
{
    public class Logs
    {
        static string folderName = @"C:\Program Files (x86)\VAPOR Client";
        static string pathString = @"C:\Program Files (x86)\VAPOR Client\Logs";
        static string fileName = "VAPORlogs.txt";
        public static void CreateFolder()
        {
            Directory.CreateDirectory(pathString);
            pathString = System.IO.Path.Combine(pathString, Logs.fileName);
            if (!System.IO.File.Exists(pathString))
            {
                System.IO.FileStream log = System.IO.File.Create(pathString);
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }
        }
        



    }
}
