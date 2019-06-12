using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03DelegateLogs
{
    class Program
    {
        public delegate void LoggerDelegate(string msg);

        public static void LogToScreen(string m)
        {
            Console.WriteLine("Screen: " + m);
        }

        public static void LogFancy(string mmm)
        {
            Console.WriteLine("FAAAAAAAAAAAAAANCY: " + mmm);
        }

        public static void LogToFile(string message)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            File.AppendAllText(@"..\..\log.txt", $"{now}: {message}{Environment.NewLine}");
            DateTime dt = new DateTime();
        }

        static void Main(string[] args)
        {
            try
            {
                LoggerDelegate logger = null;

                logger = LogToScreen;
                logger += LogFancy;
                logger += LogToFile;

                // these two lines are equivalent
                logger?.Invoke("Something happened"); // nullable expression
                if (logger != null) logger("Something happened"); // if null
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }
    }
}
