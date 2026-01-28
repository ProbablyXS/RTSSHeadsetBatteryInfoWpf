using RTSSSharedMemoryNET;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using FontStyle = System.Drawing.FontStyle;

namespace RTSSHeadsetBatteryInfo
{
    public class HeadsetService
    {
        public static string OsdName = "HeadsetBattery";
        public bool _running;
        public event Action<ConsoleMessageType, string>? OnLog;

        private enum Status
        {
            BATTERY_UNAVAILABLE,
            BATTERY_CHARGING,
            BATTERY_AVAILABLE
        }

        private readonly MainWindow _mainWindow;
        public HeadsetService(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }


        public void Start()
        {
            _running = true;

            Log(ConsoleMessageType.Menu, $"[Running overlay {OsdName}]");

            new Thread(async () =>
            {
                using var osd = new OSD(OsdName);

                while (_running)
                {
                    var (name, battery, status, isConnected) = GetHeadsetInfo();

             string text = status switch
    {
        Status.BATTERY_AVAILABLE => $"{name} Battery: {battery}%",
        Status.BATTERY_CHARGING => $"{name} Battery: {battery}% Charging",
        Status.BATTERY_UNAVAILABLE when isConnected => $"{name} Disconnected",
        _ => $"{name} is turned off"
    };

                    _ = Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        if (name != "Unknown") 
                        {
                            _mainWindow.TrayIcon.ToolTipText = text;
                        }
                        else
                        {
                            _mainWindow.TrayIcon.ToolTipText = _mainWindow.Title;
                        }

                        UpdateTrayIcon(battery);
                    });

                    if (name == "Unknown") continue;
                    osd.Update(text);
                    Log(ConsoleMessageType.Info, text);

                    await Task.Delay(2000);
                }
            })
            { IsBackground = true }.Start();
        }


        public void Stop()
        {
            _running = false;
            Log(ConsoleMessageType.Warning, "Overlay stopped");
        }

        private void Log(ConsoleMessageType type, string text)
        {
            OnLog?.Invoke(type, $"{DateTime.Now:HH:mm:ss} {text}");
        }

        private static (string name, int battery, Status status, bool isConnected) GetHeadsetInfo()
        {
            try
            {
                var exePath = Path.Combine(
       AppDomain.CurrentDomain.BaseDirectory,
       "headsetcontrol.exe"
   );

                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = exePath,
                        Arguments = "-o json",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };


                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "headsetcontrol.exe",
                        Arguments = "--connected",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                p.Start();
                bool isConnected = Convert.ToBoolean(p.StandardOutput.ReadToEnd());
                p.WaitForExit();

                using var json = JsonDocument.Parse(output);
                var device = json.RootElement.GetProperty("devices")[0];


                var statusString = device
                    .GetProperty("battery")
                    .GetProperty("status")
                    .GetString();

                if (!Enum.TryParse(statusString, ignoreCase: true, out Status status))
                {
                    status = Status.BATTERY_UNAVAILABLE;
                }

                return (
                    device.GetProperty("device").GetString() ?? "Headset",
                    device.GetProperty("battery").GetProperty("level").GetInt32(),
                    status,
                    isConnected
                );

            }
            catch
            {
                return ("Unknown", 0, Status.BATTERY_UNAVAILABLE, false);
            }
        }

        private int? _lastBattery = null;

        private void UpdateTrayIcon(int battery)
        {
            if (_lastBattery == battery) return;
            _lastBattery = battery;

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (battery <= 0)
                {
                    _mainWindow.TrayIcon.IconSource = new BitmapImage(new Uri("pack://application:,,,/Resources/app.ico"));
                }
                else
                {
                    _mainWindow.TrayIcon.IconSource = CreateNumberIcon(battery);
                }
            });
        }

        public static BitmapSource CreateNumberIcon(int number)
        {
            int size = 16;
            using var bmp = new Bitmap(size, size);
            using var g = Graphics.FromImage(bmp);

            g.Clear(Color.Transparent);

            Color color = number < 20 ? Color.Red : Color.White;
            using var brush = new SolidBrush(color);
            using var font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Pixel);

            var text = number != -1 ? number.ToString() : "Off";
            var textSize = g.MeasureString(text, font);
            float x = (size - textSize.Width) / 2;
            float y = (size - textSize.Height) / 2;

            g.DrawString(text, font, brush, x, y);


            var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }



    }
}
