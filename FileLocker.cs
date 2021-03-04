using System;
using System.IO;

namespace PBWatchdog
{
    class FileLocker
    {
        private static readonly string pboxpath = "pbox.txt";
        private static FileStream pbox;
        private static readonly string userName = Environment.UserName;

        public static void Lock()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (ConfigFiles.GetUserValueBool("ShowNameInMonitor"))
                {
                    File.WriteAllText(pboxpath, userName); //writes Username in pbox.txt before file gets locked
                    Logger.Log("wrote username in pbox.txt");
                }
                else
                {
                    File.WriteAllText(pboxpath, "");
                }
                pbox = new FileStream(pboxpath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read); //creates file and keeps it open
                Logger.Log("pbox.txt created/locked");
            }
            catch
            {
                Logger.Log("cannot lock pbox.txt: already in use");
            }
        }
        public static bool Check()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                File.AppendAllText(pboxpath, "");
                Logger.Log("pbox.txt is not in use");
                return false;
            }
            catch
            {
                Logger.Log("pbox.txt is already in use");
                return true;
            }
        }
        public static void Unlock()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (pbox != null)
            {
                try
                {
                    pbox.Close();
                    Logger.Log("pbox.txt unlocked");
                    File.WriteAllText(pboxpath, "");
                }
                catch
                {

                }
            }
        }
    }
}
