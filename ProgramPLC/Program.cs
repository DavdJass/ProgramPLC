using ProgramPLC.Database;
using ProgramPLC.Parallelism.ConsoleApp1.Tests;
using ProgramPLC.Services;

namespace ProgramPLC
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new AnalysisPlcContext();
            // BuildServices buildServices = new BuildServices(context);

            //var serviceDevice = buildServices.ServiceDevice();
            //var serviceInit = buildServices.ServiceInitMonitor();
            PLCservice plc = new PLCservice();

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            //CONDITION, IF CONNECTION IS SUCCESS(1 OR ANOTHER NUMBER REPRESENTS "Logical Number Station" FROM PLC) MONITORING PROCESS BEGINS
            if (PLCservice.ConnectionPLC(1))
            {
                //var processTask = await ThreadFactory.Create();
                var processTask = ThreadFactory.Create();
                // Esperamos el resultado de la tarea
                List<Thread> process = processTask;

                foreach (var t in process)
                {
                    t.Start();
                }
            }
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("La aplicación está a punto de cerrarse.");
            PLCservice.CloseConnectionPLC();
        }


    }
}