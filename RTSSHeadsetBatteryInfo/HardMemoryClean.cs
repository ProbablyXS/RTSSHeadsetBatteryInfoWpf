using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

public static class MemoryHelper
{
    [DllImport("psapi.dll")]
    private static extern bool EmptyWorkingSet(IntPtr hProcess);

    public static void HardMemoryClean()
    {
        System.Runtime.GCSettings.LargeObjectHeapCompactionMode =
            System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;

        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);
        GC.WaitForPendingFinalizers();
        EmptyWorkingSet(Process.GetCurrentProcess().Handle);
    }

    public static async Task StartAutoClean(Window window)
    {
            while (true)
            {

            await Task.Delay(5000);

                window.Dispatcher.Invoke(() =>
                {
                    if (window.Visibility == Visibility.Hidden)
                    {
                        HardMemoryClean();
                    }
                });
            }
    }
}
