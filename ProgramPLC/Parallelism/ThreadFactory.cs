namespace ProgramPLC.Parallelism
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using ProgramPLC.Database;

    namespace ConsoleApp1.Tests
    {
        public static class ThreadFactory
        {
            private static int _currentIndex;
            private static int _currentDB = 0;
            private static List<DeviceMonitor> _hlObj;
            private static List<DeviceMonitor> Init()
            {
                //AnalysisPlcContext context = new AnalysisPlcContext();
                BuildServices services = new BuildServices();
                
                List<DeviceMonitor> threads = new List<DeviceMonitor>();
                //EXCEPTION
                
                    var table = services.ServiceInitMonitor().GetActived();
                    foreach (var item in table.Result)
                    {
                        int engine = Convert.ToInt32(item.InitMonitorId);
                        string Hertz = item.Hertz;
                        string Volts = item.Volts;
                        string Amper = item.Amper;

                        var t1 = new DeviceMonitor(engine, Hertz);
                        var t2 = new DeviceMonitor(engine, Volts);
                        var t3 = new DeviceMonitor(engine, Amper);

                        threads.Add(t1);
                        threads.Add(t2);
                        threads.Add(t3);
                    }
                
                
                
                _hlObj = threads;
                return threads;
            }
            public static  List<Thread> Create()
            {
                //AnalysisPlcContext context = new AnalysisPlcContext();
                BuildServices services = new BuildServices();

                var data = Init();
                Console.WriteLine(data.Count);
                if (data == null)
                    return null;

                List<Thread> threads = new List<Thread>();

                for (int i = 0; i < data.Count; i++)
                {
                    int index = i;
                    Thread t1 = new Thread(() => data[index].DeviceMonitorThread());
                    threads.Add(t1);
                }

                return threads;
            }
            public static void CreateOne()
            {
                //AnalysisPlcContext context = new AnalysisPlcContext();
                BuildServices services = new BuildServices();

                var table = services.ServiceInitMonitor().GetActived();

                int currentDB = table.Result.Count;
                int indx = 0;
                if (currentDB != _currentDB)
                {
                    Console.WriteLine("Rows DB" + table.Result.Count);
                    Console.WriteLine("Currebt DB" + _currentDB);
                    _currentDB = currentDB;

                    int engine = Convert.ToInt32(table.Result[_currentIndex + 1].InitMonitorId);
                    string Hertz = table.Result[_currentIndex + 1].Hertz.ToString();
                    string Volts = table.Result[_currentIndex + 1].Volts.ToString();
                    string Amper = table.Result[_currentIndex + 1].Amper.ToString();
                    var t1 = new DeviceMonitor(engine, Hertz);
                    var t2 = new DeviceMonitor(engine, Volts);
                    var t3 = new DeviceMonitor(engine, Amper);

                    _hlObj.Add(t1);
                    _hlObj.Add(t2);
                    _hlObj.Add(t3);

                    Thread tthr1 = new Thread(() => t1.DeviceMonitorThread());
                    Thread tthr2 = new Thread(() => t2.DeviceMonitorThread());
                    Thread tthr3 = new Thread(() => t3.DeviceMonitorThread());

                    indx = _currentIndex + 1;
                    _currentIndex = indx;

                    tthr1.Start();
                    tthr2.Start();
                    tthr3.Start();
                }

            }

        }
    }

}
