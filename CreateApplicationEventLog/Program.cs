using System;
using System.Diagnostics;

namespace CreateApplicationEventLog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!EventLog.SourceExists("InstrumentationHandsOn", Environment.MachineName))
                {
                    EventLog.CreateEventSource("InstrumentationHandsOn", "CreateApplicationEventLog");
                }
                EventLog LogDemo = new EventLog("CreateApplicationEventLog", Environment.MachineName, "InstrumentationHandsOn");
                LogDemo.WriteEntry("Event Written to Application Log", EventLogEntryType.Information, 234, Convert.ToInt16(3));

                Trace.AutoFlush = true;
                EventLogTraceListener MyListener = new EventLogTraceListener(LogDemo);
                Trace.WriteLine("This is a test");

                Console.WriteLine(LogDemo.Log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
