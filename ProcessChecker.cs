using System;
using System.Diagnostics;
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
    }
}
