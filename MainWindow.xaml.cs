using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace PBWatchdog
{
    /// <summary>
    /// Mainwindow of this application. Checks if a specific process is running und synchronizes via textfile. 
    /// This application is supposed to run via Network-Drive.
    /// </summary>
    public partial class MainWindow : Window
    {
        bool remind = true, notify = false, kill; //to send reminder that process is still not running, to not show notification on start, if process needs to be killed
        private DispatcherTimer refreshTimer; //intervall to check if process is running
        private DispatcherTimer remindTimer; //timer to send notification if process is closed für x min
        private string windowTitle, processName;
        Settings settings; //creating object settings so it already exists and for checking if window is already open
        public MainWindow()
        {
            FileHandler.CreateDir();
            FileHandler.CreateUserConfig();
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded); //Calls Window_Loaded Method after Main Window is loaded
            tb.Icon = Properties.Resources.pbizicon;
            Refresh_Timer();
            Remind_Timer();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            LoadWindowPosition();
            ConfigFiles.ReadAppConfig();
            ConfigFiles.UserConfig();
            IsPBRunning(sender, e);
        }
        #region MainFunction
        public void IsPBRunning(object sender, EventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            CheckKill(); //Checks if Process needs to be killed via config
            windowTitle = ConfigFiles.GetWindowTitle();
            processName = ConfigFiles.GetProcessName();
            bool pbio = ProcessChecker.IsProcessRunning(processName, windowTitle);
            if (pbio)
            {
                btn_PB.Background = Brushes.Green;
                btn_PB.Content = ConfigFiles.GetWindowTitle();
                tb.Icon = Properties.Resources.pbioicon;
                remindTimer.Stop();
                remind = false;
                notify = true;
                txbUserName.Text = File.ReadAllText("pbox.txt");
                Logger.Log("remind timer stopped");
            }
            else
            {
                btn_PB.Background = new SolidColorBrush(Color.FromRgb(198, 40, 40));
                btn_PB.Content = ConfigFiles.GetWindowTitle();
                Logger.Log("PBIZ");
                tb.Icon = Properties.Resources.pbizicon;
                remind = true;
                if (notify)
                {
                    ShowCustomNotification();
                    notify = false;
                }
                PBIZTimer();
                txbUserName.Text = "";
            }
        }
        #region Helper
        private void CheckKill()
        {
            kill = ConfigFiles.GetKill();
            if (kill)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log("application was shut down");
                Application.Current.Shutdown();
            }
        }
        private void btn_PB_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                var p = new Process();
                p.StartInfo.FileName = ConfigFiles.GetProcessPath();
                p.StartInfo.Arguments = ConfigFiles.GetProcessArgument();
                p.Start();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }

        }
        #endregion
        #endregion

        #region UI Controlls
        //for dragging the window
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
                string topPosition = Convert.ToString(this.Top);
                string leftPosition = Convert.ToString(this.Left);
                ConfigFiles.SetUserValue("TopPosition", topPosition);
                ConfigFiles.SetUserValue("LeftPosition", leftPosition);

            }
            catch (Exception ex)
            {
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Logger.Log("application was shut down");
            Application.Current.Shutdown();
        }

        private void HideMain_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            App.Current.MainWindow.Hide();
            Logger.Log("main window hidden");
        }
        public void ShowMain_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            App.Current.MainWindow.Show();
            Logger.Log("main window shown");
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (settings == null || settings.IsVisible == false)
            {
                settings = new Settings();
            }
            Logger.Log("settings opened");
        }
        private void LoadWindowPosition()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            string top = ConfigFiles.GetUserValue("TopPosition");
            string left = ConfigFiles.GetUserValue("LeftPosition");
            if (top == "" && left == "")
            {
                var desktopWorkingArea = SystemParameters.WorkArea; //Creates Desktop Area
                this.Left = desktopWorkingArea.Right - this.Width; //gets Desktop width legth
                this.Top = desktopWorkingArea.Bottom - this.Height; //gets Desktop height length
            }
            else
            {
                this.Top = Convert.ToDouble(top);
                this.Left = Convert.ToDouble(left);
            }
        }
        private void TrayExportLog_Click(object sender, RoutedEventArgs e)
        {
            FileHandler.ExportLog();
        }
        #endregion

        #region Timer
        private void PBIZTimer()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (remind)
            {
                remindTimer.Start();
                remind = false;
                Logger.Log("remind timer started");
            }
        }
        public void PBIZTimer_Remind(object sender, EventArgs e)
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            ShowCustomNotification();
            Logger.Log("show remind notification");
        }
        private void Refresh_Timer()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            refreshTimer = new DispatcherTimer();
            refreshTimer.Tick += new EventHandler(IsPBRunning);
#if DEBUG
            refreshTimer.Interval = new TimeSpan(0, 0, 3);
#endif
#if RELEASE
            
            refreshTimer.Interval = new TimeSpan(0, 0, ConfigFiles.GetTimer());
#endif
            refreshTimer.Start();
            Logger.Log("refresh timer started");
        }
        private void Remind_Timer()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            remindTimer = new DispatcherTimer();
            remindTimer.Tick += new EventHandler(PBIZTimer_Remind);
            remindTimer.Interval = new TimeSpan(0, 5, 0);
        }
        #endregion

        #region Notification
        private static void ShowCustomNotification()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (!ConfigFiles.GetUserValueBool("NotificationWeekend") && ShowNotificationOnWeekend() && ShowNotificationAtThisTime())
            {
                //in this case no notification
            }
            else
            {
                if (ShowNotificationAtThisTime())
                {
                    Notification notification = new Notification();
                    notification.Show();
                    Logger.Log("show notification");
                }
            }
        }
        private static bool ShowNotificationOnWeekend()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            if ((Convert.ToString(DateTime.Today.DayOfWeek) == "Saturday" || Convert.ToString(DateTime.Today.DayOfWeek) == "Sunday"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool ShowNotificationAtThisTime()
        {
            Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
            string[] startTime = ConfigFiles.GetUserValue("NotificationStartTime").Split(":");
            string[] stopTime = ConfigFiles.GetUserValue("NotificationStopTime").Split(":");
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan start = new TimeSpan(Convert.ToInt32(startTime[0]), Convert.ToInt32(startTime[1]), 0);
            TimeSpan end = new TimeSpan(Convert.ToInt32(stopTime[0]), Convert.ToInt32(stopTime[1]), 0);
            if ((currentTime > start) && (currentTime < end))
            {
                Logger.Log("time is within notification time");
                return true;
            }
            else
            {
                Logger.Log("time is not within notification time");
                return false;
            }
        }
        #endregion
    }
}