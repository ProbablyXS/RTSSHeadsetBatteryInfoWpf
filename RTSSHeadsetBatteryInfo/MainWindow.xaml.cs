using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RTSSHeadsetBatteryInfo
{
    public partial class MainWindow : Window
    {
        private readonly HeadsetService _service;

        public MainWindow()
        {
            InitializeComponent();
            _service = new HeadsetService(this);
            _service.OnLog += AddLog;
            _service.Start();
            _ = MemoryHelper.StartAutoClean(this);
            Hide();
        }

        #region Boutons Start / Stop


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (_service._running) { return; }
            _service.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (!_service._running) { return; }
            _service.Stop();
            TrayIcon.IconSource = new BitmapImage(new Uri("pack://application:,,,/Resources/app.ico"));
        }

        #endregion

        #region Logs

        private void AddLog(ConsoleMessageType type, string text)
        {
            Dispatcher.Invoke(() =>
            {
                LogPanel.Children.Add(new TextBlock
                {
                    Text = text,
                    Foreground = type.Color
                });

                LogScrollViewer.ScrollToEnd();
            });
        }

        #endregion

        #region Tray icon

        private void Tray_Open_Click(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void Tray_Exit_Click(object sender, RoutedEventArgs e)
        {
            TrayIcon.Dispose();
            _service.Stop();
            Application.Current.Shutdown();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            // Minimise dans le tray
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            TrayIcon.Dispose();
            base.OnClosed(e);
        }

        #endregion
    }
}
