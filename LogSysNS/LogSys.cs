using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogSysNS
{
    public static class LogSys
    {
        public static void WriteToLogFile(string message)
        {

            using (StreamWriter sw = new StreamWriter("log.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(message);
            }
           
        }
    }
}
