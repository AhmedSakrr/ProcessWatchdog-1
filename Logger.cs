using System;
using System.IO;
using System.Diagnostics;

namespace PBWatchdog
{
    public class Logger
    {
        private static readonly string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string filePath = $"{userProfile}/ProcessWatchdog/Logs/log.txt";
        private static readonly string backupFilePath = $"{userProfile}/ProcessWatchdog/Logs/Backup/backupLog.txt";
        private static readonly int logLevel = ConfigFiles.GetLogLevel();

        public static void Log(string message)
        {
            if (logLevel == 0)
            {
                try
                {
                    File.AppendAllText(filePath, $"{DateTime.Now} INFO MESSAGE:\t{message}\n"); //writes to textfile
                    IsLogTooBig();
                }
                catch (Exception ex)
                {
                    using EventLog eventLog = new EventLog("Application")
                    {
                        Source = "Application"
                    };
                    eventLog.WriteEntry(Convert.ToString(ex), EventLogEntryType.Information, 101, 1);
                }
            }
        }
        public static void Log(string name, string current)
        {
            if (logLevel == 0 | logLevel == 1)
            {
                try
                {
                    File.AppendAllText(filePath, $"{DateTime.Now} INFO {name}: \t{current}\n"); //writes to textfile
                    IsLogTooBig();
                }
                catch (Exception ex)
                {
                    using EventLog eventLog = new EventLog("Application")
                    {
                        Source = "Application"
                    };
                    eventLog.WriteEntry(Convert.ToString(ex), EventLogEntryType.Information, 101, 1);
                }
            }
        }
        public static void Log(Exception ex)
        {
            try
            {
                File.AppendAllText(filePath, $"{DateTime.Now} ERROR:\t {Convert.ToString(ex)}\n"); //writes to textfile
                IsLogTooBig();
            }
            catch (Exception e)
            {
                using EventLog eventLog = new EventLog("Application")
                {
                    Source = "Application"
                };
                eventLog.WriteEntry(Convert.ToString(ex), EventLogEntryType.Information, 101, 1);
                eventLog.WriteEntry(Convert.ToString(e), EventLogEntryType.Information, 101, 1);
            }
        }
        public static void IsLogTooBig()
        {
            FileHandler.IsFileTooBig(filePath, backupFilePath, 1000000);
        }
    }
}