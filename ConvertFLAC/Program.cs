using System;
using System.Diagnostics;
using System.IO;

namespace ConvertFLAC
{
    public static class Program
    {
        public static string runFFmpeg(string argument)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                Arguments = argument,
                CreateNoWindow = true,
                FileName = "ffmpeg.exe",
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using (Process process = new Process()
            {
                StartInfo = startInfo
            })
            {
                process.Start();
                string ret = process.StandardError.ReadToEnd();
                process.WaitForExit();
                return ret;
            }
        }

        private static void pause()
        {
            Console.Write("請按任意鍵繼續 ...");
            Console.ReadKey(true);
        }

        public static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.Error.WriteLine("請指定輸入檔案");
                pause();
                return;
            }

            foreach (string file in args)
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    Console.WriteLine(runFFmpeg($"-y -i \"{file}\" -ar 16000 {name}.flac"));
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"轉換檔案時發生例外狀況 : {ex.Message}");
                }
            }
            pause();
        }
    }
}