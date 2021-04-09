using System;
using System.Configuration;

namespace PBWatchdog
{
    public class ConfigFiles
    {
        private static string windowTitle, processName; //name of WindowTitle and Process you want to check
        private static readonly string mainFileName = "app.config"; 
        private static readonly int userConfigKeyCount = 8; //how many keys are in the UserConfig to check if everything is there 
        
        public static string GetWindowTitle()
        {
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = mainFileName
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                windowTitle = config.AppSettings.Settings["WindowTitle"].Value; //Gets Config Value
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
            return windowTitle;
        }
        public static string GetProcessName()
        {
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = mainFileName
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                processName = config.AppSettings.Settings["ProcessName"].Value; //Gets Config Value
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
            return processName;
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
        public static int GetLogLevel()
        {
            int logLevel = 0;
            try
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = mainFileName
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                logLevel = Convert.ToInt32(config.AppSettings.Settings["LogLevel"].Value);
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
            return logLevel;
        }
        public static void GetUserConfig()
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