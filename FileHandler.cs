using IWshRuntimeLibrary;
using System;
using System.IO;

namespace PBWatchdog
{
    public class FileHandler
    {
        private static readonly string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string dir = $"{userProfile}/PBWatchdog/";
        private static readonly string logdir = $"{userProfile}/PBWatchdog/Logs/";
        private static readonly string backuplogdir = $"{userProfile}/PBWatchdog/Logs/Backup/";
        public static readonly string userconfigfile = $"{userProfile}/PBWatchdog/Config/user.config";
        private static readonly string userconfigdir = $"{userProfile}/PBWatchdog/Config/";
        private static readonly string autostartpath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        public static void CreateDir()
        {
            try
            {
                Directory.CreateDirectory(dir);
                Directory.CreateDirectory(logdir);
                Directory.CreateDirectory(backuplogdir);
                Directory.CreateDirectory(userconfigdir);
                Logger.Log("directorys created");
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static void IsFileTooBig(string path, string backupPath, int size)
        {
            try
            {
                if (new FileInfo(path).Length > size)
                {
                    System.IO.File.Delete(backupPath);
                    System.IO.File.Move(path, backupPath);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static void CreateUserConfig()
        {
            try
            {
                if (System.IO.File.Exists(userconfigfile))
                {
                    Logger.Log("user.config already exists");
                    if (!ConfigFiles.CheckUserConfig())
                    {
                        System.IO.File.Delete(userconfigfile);
                        System.IO.File.Copy("user.config", userconfigfile);
                    }
                }
                else
                {
                    System.IO.File.Copy("user.config", userconfigfile);
                    Logger.Log("user.config copied to appdata/local");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static void CreateShortcut(string shortcutName, string targetFileLocation)
        {
            try
            {
                string shortcutLocation = Path.Combine(autostartpath, shortcutName + ".lnk");
                if (!System.IO.File.Exists(shortcutLocation))
                {
                    WshShell shell = new();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
                    shortcut.WorkingDirectory = Environment.CurrentDirectory; //The directory where the file you want to start is in 
                    shortcut.TargetPath = targetFileLocation; // The path of the file that will launch when the shortcut is run
                    shortcut.Save(); // Save the shortcut
                    Logger.Log("shortcut created");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static void DeleteShortcut(string fileName)
        {
            try
            {
                if (System.IO.File.Exists(autostartpath + fileName))
                {
                    System.IO.File.Delete(autostartpath + fileName);
                    Logger.Log("shortcut deleted");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static void ExportLog()
        {
            try
            {
                if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $@"\Logs_{Environment.UserName}.zip"))
                {
                    System.IO.Compression.ZipFile.CreateFromDirectory(logdir, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $@"\Logs_{Environment.UserName}.zip");
                    Logger.Log("log exported to desktop");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
    }
}