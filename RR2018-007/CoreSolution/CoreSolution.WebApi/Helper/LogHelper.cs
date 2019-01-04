using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Helper
{
    public class LogHelper
    {
        public static void WriteLog(string content)
        {
            string filePath = @"D:\WebIIS\PostStationWebApi\logs\mylogs.txt";
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + content + "\r\n");
            }
        }
    }
}
