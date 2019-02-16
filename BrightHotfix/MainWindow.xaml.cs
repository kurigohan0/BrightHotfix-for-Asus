using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Threading;

namespace BrightHotfix
{
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = null;

        public MainWindow()
        {
            InitializeComponent();
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            notifyIcon.Icon = new System.Drawing.Icon(@"brightness.ico");

            notifyIcon.Visible = true;
            ShowTrayIcon(!IsVisible);
            this.Visibility = Visibility.Hidden;
            this.ShowInTaskbar = false;

            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(timer_tick);
        }

        void ShowTrayIcon(bool show)
        {
            if (notifyIcon != null)
                notifyIcon.Visible = show;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Disable();
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            Enable();
        }

        DispatcherTimer timer = new DispatcherTimer();        
        Process devManViewProc = new Process();

        public void Disable()
        {
            devManViewProc.StartInfo.FileName = @"DevManView.exe";
            devManViewProc.StartInfo.Arguments = "/disable \"Intel(R) UHD Graphics 620\"";
            devManViewProc.Start();
            devManViewProc.WaitForExit();
        }


        public void Enable()
        {
            devManViewProc.StartInfo.Arguments = "/enable \"Intel(R) UHD Graphics 620\"";
            devManViewProc.Start();
            devManViewProc.WaitForExit();
            timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Disable();
            timer.Start();
        }
    }
}
