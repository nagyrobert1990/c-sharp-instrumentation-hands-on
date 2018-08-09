using System;
using System.Diagnostics;
using System.Timers;

/*
if it doesn't works:
Click Start, type cmd right click cmd.exe, and select Run as administrator.
At the prompt, type lodctr /r and press ENTER. This will repair the pointers (those are stored in the registry).

message will be:
Info: Successfully rebuilt performance counter setting from system backup store
*/
namespace MonitoringApplicationPerformance
{
    class Program
    {
        private static PerformanceCounter HeapCounter = null;
        private static PerformanceCounter ExceptionCounter = null;
        private static Timer DemoTimer;

        static void Main(string[] args)
        {
            DemoTimer = new Timer(3000);
            DemoTimer.Elapsed += new ElapsedEventHandler(OnTick);
            DemoTimer.Enabled = true;
            try
            {
                HeapCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps")
                {
                    InstanceName = "_Global_"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            ExceptionCounter = new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown")
            {
                InstanceName = "_Global_"
            };

            Console.WriteLine("Press [Enter] to Quit Program");
            Console.ReadLine();            
        }

        private static void OnTick(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("# of Bytes in all Heaps : " + HeapCounter.NextValue().ToString());
            Console.WriteLine("# of Framework Exceptions Thrown : " + ExceptionCounter.NextValue().ToString());
        }
    }
}
