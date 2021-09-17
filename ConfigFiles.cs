using System;
using System.Configuration;

namespace PBWatchdog
{
    public class ConfigFiles
    {
        private static string windowTitle, processName, processPath, processArgument; 
        private static readonly string mainFileName = "app.config";
        private static bool invertColors;
        private static readonly int userConfigKeyCount = 9; //how many keys are in the UserConfig to check if everything is there
        private static int time = 30, logLevel = 0;

        public static void ReadAppConfig()
        {
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = mainFileName
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                processName = config.AppSettings.Settings["ProcessName"].Value; //Gets Config Value
                windowTitle = config.AppSettings.Settings["WindowTitle"].Value;
                time = Convert.ToInt32(config.AppSettings.Settings["Timer"].Value);
                logLevel = Convert.ToInt32(config.AppSettings.Settings["LogLevel"].Value);
                processPath = config.AppSettings.Settings["ProcessPath"].Value;
                processArgument = config.AppSettings.Settings["ProcessArgument"].Value;
                invertColors = Convert.ToBoolean(config.AppSettings.Settings["InvertColors"].Value);
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static string GetWindowTitle()
        {
            return windowTitle;
        }
        public static string GetProcessName()
        {
            return processName;
        }
        public static int GetTimer()
        {
            return time;
        }
        public static int GetLogLevel()
        {
            return logLevel;
        }
        public static string GetProcessPath()
        {
            return processPath;
        }
        public static string GetProcessArgument()
        {
            return processArgument;
        }
        public static bool GetInvertColors()
        {
            return invertColors;
        }
        public static bool GetKill()
        {
            bool kill = false;
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = mainFileName
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                if (config.AppSettings.Settings["Kill"].Value == "true")
                {
                    kill = true;
                }
                else
                {
                    kill = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
            return kill;
        }
        public static void UserConfig()
        {
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = FileHandler.userconfigfile
                };
                Configuration userconfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                if (userconfig.AppSettings.Settings["HideMainWindowOnStart"].Value == "true")
                {
                    App.Current.MainWindow.Hide();
                }
                if (userconfig.AppSettings.Settings["Autostart"].Value == "true")
                {
                    FileHandler.CreateShortcut("PBWatchdog", Environment.CurrentDirectory + @"\PBWatchdog.exe");
                }
                else
                {
                    FileHandler.DeleteShortcut("PBWatchdog.lnk");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static void SetUserValue(string key, string value)
        {
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = FileHandler.userconfigfile
                };
                Configuration userconfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                userconfig.AppSettings.Settings[key].Value = value;
                userconfig.Save();
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        public static bool GetUserValueBool(string key)
        {
            bool value = false;
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = FileHandler.userconfigfile
                };
                Configuration userconfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                if (userconfig.AppSettings.Settings[key].Value == "true")
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
            return value;
        }
        public static string GetUserValue(string key)
        {
            string value = "";
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = FileHandler.userconfigfile
                };
                Configuration userconfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                value = userconfig.AppSettings.Settings[key].Value;
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
            return value;
        }
        public static bool CheckUserConfig()
        {
            bool check = false;
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = FileHandler.userconfigfile
                };
                Configuration userconfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                if (Convert.ToInt32(userconfig.AppSettings.Settings.Count) == userConfigKeyCount)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
            return check;
        }
    }
}