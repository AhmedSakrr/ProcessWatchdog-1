using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace PBWatchdog
{
    /// <summary>
    /// Interaktionslogik für Config.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove(); //For dragging the window
            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("settings closed by user");
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            bool close = true;
            //Autostart
            if (cBoxAutostart.IsChecked ?? true)
            {
                ConfigFiles.SetUserValue("Autostart", "true");
                FileHandler.CreateShortcut("ProcessWatchdog", Environment.CurrentDirectory + @"\ProcessWatchdog.exe");
            }
            else
            {
                ConfigFiles.SetUserValue("Autostart", "false");
                FileHandler.DeleteShortcut("ProcessWatchdog.lnk");
            }
            //Show monitor
            if (cBoxMonitor.IsChecked ?? true)
            {
                ConfigFiles.SetUserValue("HideMainWindowOnStart", "true");
            }
            else
            {
                ConfigFiles.SetUserValue("HideMainWindowOnStart", "false");
            }
            //Show name in monitor
            if (cBoxShowNameInMonitor.IsChecked ?? true)
            {
                if (!ConfigFiles.GetUserValueBool("ShowNameInMonitor"))
                {
                    ConfigFiles.SetUserValue("ShowNameInMonitor", "true");
                    ShowInfoBox();
                }
            }
            else
            {
                if (ConfigFiles.GetUserValueBool("ShowNameInMonitor"))
                {
                    ConfigFiles.SetUserValue("ShowNameInMonitor", "false");
                    ShowInfoBox();
                }
            }
            //Start time
            if (TimeValidation(txbTimeStart.Text))
            {
                ConfigFiles.SetUserValue("NotificationStartTime", txbTimeStart.Text);
            }
            else
            {
                close = false;
            }
            //Stop time
            if (TimeValidation(txbTimeStop.Text))
            {
                ConfigFiles.SetUserValue("NotificationStopTime", txbTimeStop.Text);
            }
            else
            {
                close = false;
            }
            //Weekend notifications
            if (cBoxWeekendNotification.IsChecked ?? true)
            {
                ConfigFiles.SetUserValue("NotificationWeekend", "true");
            }
            else
            {
                ConfigFiles.SetUserValue("NotificationWeekend", "false");
            }
            //No notifications
            if (cBoxDeactivateNotification.IsChecked ?? true)
            {
                ConfigFiles.SetUserValue("NoNotification", "true");
            }
            else
            {
                ConfigFiles.SetUserValue("NoNotification", "false");
            }
            //To see if everything is valid
            if (close)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Keine korrekte Zeitangabe!", "Eingabe überprüfen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadSettings()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (ConfigFiles.GetUserValueBool("Autostart"))
            {
                cBoxAutostart.IsChecked = true;
            }
            else
            {
                cBoxAutostart.IsChecked = false;
            }
            if (ConfigFiles.GetUserValueBool("HideMainWindowOnStart"))
            {
                cBoxMonitor.IsChecked = true;
            }
            else
            {
                cBoxMonitor.IsChecked = false;
            }
            if (ConfigFiles.GetUserValueBool("ShowNameInMonitor"))
            {
                cBoxShowNameInMonitor.IsChecked = true;
            }
            else
            {
                cBoxShowNameInMonitor.IsChecked = false;
            }

            txbTimeStart.Text = ConfigFiles.GetUserValue("NotificationStartTime");
            txbTimeStop.Text = ConfigFiles.GetUserValue("NotificationStopTime");

            if (ConfigFiles.GetUserValueBool("NotificationWeekend"))
            {
                cBoxWeekendNotification.IsChecked = true;
            }
            else
            {
                cBoxWeekendNotification.IsChecked = false;
            }
            if (ConfigFiles.GetUserValueBool("NoNotification"))
            {
                cBoxDeactivateNotification.IsChecked = true;
            }
            else
            {
                cBoxDeactivateNotification.IsChecked = false;
            }
        }
        private static bool TimeValidation(string text)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            const string pattern = "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            bool valid = Regex.IsMatch(text, pattern);
            return valid;
        }
        private static void ShowInfoBox()
        {
            MessageBox.Show("Damit die Änderungen wirksam werden, muss der ProcessWatchdog einmal neu gestartet werden.", "Neustart erforderlich!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
