using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PBWatchdog
{
    /// <summary>
    /// Interaktionslogik für Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        private string title, message;
        private DispatcherTimer dispatcherTimer;
        public Notification(string title, string message)
        {
            Logger.Log("CLASS", this.ToString());
            InitializeComponent();
            this.title = title;
            this.message = message;
            this.Loaded += new RoutedEventHandler(Window_Loaded); //Calls Window_Loaded Method after Main Window is loaded
            OpenAnimation();
            Timer();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txbTitle.Text = title;
                txbMessage.Text = message;
                //to show notification in bottom right
                var desktopWorkingArea = SystemParameters.WorkArea; 
                this.Left = desktopWorkingArea.Right - this.Width; 
                this.Top = desktopWorkingArea.Bottom - this.Height; 
                Window window = (Window)sender;
                window.Topmost = true;
            }
            catch (Exception ex)
            {
                Logger.Log("CLASS", this.ToString());
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log("notification closed by user");
            CloseAnimation(sender, e);
        }

        private void Timer()
        {
            try
            {
                dispatcherTimer = new DispatcherTimer(); //Sets a new Timer
                dispatcherTimer.Tick += new EventHandler(CloseAnimation); //Calls Method
                dispatcherTimer.Interval = new TimeSpan(0, 0, 10); // Set Interval in h, m, s
                dispatcherTimer.Start(); //Starts Timer
            }
            catch (Exception ex)
            {
                Logger.Log("CLASS", this.ToString());
                Logger.Log("METHOD", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Log(ex);
            }
        }
        private void Close(object sender, EventArgs e)
        {
            Close();
        }
        private void CloseAnimation(object sender, EventArgs e)
        {
            Storyboard sb = Resources["CloseNotification"] as Storyboard;
            sb.Begin(Window);
        }

        private void OpenAnimation()
        {
            Storyboard sb = Resources["ShowNotification"] as Storyboard;
            sb.Begin(Window);
        }
    }
}
