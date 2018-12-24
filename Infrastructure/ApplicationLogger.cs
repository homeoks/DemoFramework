using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ApplicationLogger
    {
        private static readonly object Locker = new object();

        public static void Log(string msg, string fileName)
        {
            Task.Run(() =>
            {
                Main(msg, fileName);
            });
        }

        private static void Main(string msg, string fileName)
        {
            try
            {
                lock (Locker)
                {
                    var path = $@"{Directory.GetCurrentDirectory()}/LogFile";
                    if (File.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                        path += $"/{fileName}.txt";
                        var fs = File.Create(path);
                        fs.Close();
                    }
                    else
                        path += $"/{fileName}.txt";
                  
                    System.Console.WriteLine(msg);
                    using (var file = File.AppendText(path))
                    {
                        file.WriteLine(":--:" + DateTimeOffset.Now + ":--:" + msg);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Log(Exception ex, string fileName)
        {
            Main(
                $"-Log {DateTimeOffset.Now}: {ex.Message}.StackTrace: {ex.StackTrace}. InnerExceptionMessage: {ex.InnerException?.Message}.", "Exception"+fileName);
        }
    }
}
