using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PBWatchdog
{
    public class ProcessChecker
    {
        public static bool IsProcessRunning(string processname, string windowtitle)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            bool running = false;
            try
            {
                bool name = Process.GetProcesses().ToList().Where(x => x.ProcessName.Contains(processname) && x.SessionId == Process.GetCurrentProcess().SessionId).Any();
                bool window = Process.GetProcesses().ToList().Where(x => x.MainWindowTitle.Contains(windowtitle) && x.SessionId == Process.GetCurrentProcess().SessionId).Any();
                GetAllProcesses(); //Writes all Processes to a Textfile
                if (window && name)
                {
                    Logger.Log("pbox is running local");
                    running = true;
                    FileLocker.Lock();
                }
                else
                {
                    FileLocker.Unlock();
                    running = FileLocker.Check(); //checks if file is locked and returns bool
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return running;
        }
        private static void GetAllProcesses()
        {
            string processlistpath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/PBWatchdog/Logs/processlist.txt";
            string processlistbackuppath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/PBWatchdog/Logs/Backup/processlist.txt";
            if (ConfigFiles.GetLogLevel() == 0)
            {
                try
                {
                    Process[] processlist = Process.GetProcesses();
                    foreach (Process theprocess in processlist)
                    {
                        File.AppendAllText(processlistpath, DateTime.Now + "     " + theprocess.ProcessName + "     " + theprocess.Id + "\n");
                    }
                    File.AppendAllText(processlistpath, "--------------------------------------------------------------------------------\n");
                    FileHandler.IsFileTooBig(processlistpath, processlistbackuppath , 1000000);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }

            }

        }
    }
}
