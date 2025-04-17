using DataValidationFramework.Config;
using System.IO;
using System;

namespace DataValidationFramework.Helper
{
    public static class TestLogger
    {
        private static readonly string LogPath = AppConfig.Get("ReportTextLog");

        private static readonly object _lock = new object();

        public static void Log(string message)
        {
            lock (_lock)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogPath)!);
                File.AppendAllText(LogPath, $"{DateTime.Now:HH:mm:ss} - {message}\n");
            }
        }
    }
}