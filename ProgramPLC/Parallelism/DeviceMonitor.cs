using ProgramPLC.Database;
using ProgramPLC.Models;
using ProgramPLC.Services;

namespace ProgramPLC.Parallelism
{
    public class DeviceMonitor
    {
        private int _engine;
        public string _device;
        //private AnalysisPlcContext _context;
        private BuildServices _serviceMonitor;
        private BuildServices _serviceDevice;
        public DeviceMonitor(int engine, string device)
        {
            _engine = engine;
            _device = device;
            //_context = new AnalysisPlcContext();
            _serviceMonitor = new BuildServices();
            _serviceDevice = new BuildServices();
        }
        public async void DeviceMonitorThread()
        {
            PLCservice pLCservice = new PLCservice();

            //BUCLE DE MONITOREO
            do
            {
                bool state = PLCservice.getSatet();
                //Console.WriteLine("State: " + state);
                if (state)
                {
                    //LEE LA INFO DEL PLC
                    var data = pLCservice.ReadData(_device);
                    //string formatD = data.DeviceValue.ToString("F2");
                    double format = Convert.ToDouble(data.DeviceValue) / 100;
                    string formatString = format.ToString();
                    data.DeviceValue = formatString;
                    //Console.WriteLine(formatD);
                    //VALIDA LOS HERTZSETUP CON LOS HERTZ NORMALES
                    bool current = Validations.ValHertzSetup(_serviceMonitor.ServiceInitMonitor(), _serviceDevice.ServiceDevice(), _device, data.DeviceValue);
                    bool comparison = Validations.ValDual(data.DeviceName, data.DeviceValue.ToString());
                    //bool target = Validations.OnTarget(_serviceDevice.ServiceInitMonitor(), data.DeviceName, data.DeviceValue);
                    //SE COMPRUEBA SI ESTA ACTIVO DESDE LA BD
                    if (format > 0 || data.DeviceValue != "0")
                    {
                        if (current && comparison)
                        {
                            //SI EL VALOR DE LOS HERTZSETUP COINCIDE, SE ACTUALIZA A TRUE DESDE LA BD PARA QUE SE EMPIECE A MONITOREAR
                            Validations.UpdateStatusEngine(_serviceMonitor.ServiceInitMonitor(), data.DeviceName);
                        }
                        else
                        {
                            //EL STATUS CAMBIA A FALSE
                            Validations.UpdateStatusFalseEngine(_serviceMonitor.ServiceInitMonitor(), data.DeviceName);
                        }

                        if (Validations.IsActived(_serviceMonitor.ServiceInitMonitor(), data.DeviceName))
                        {

                            //Insert data in DataBase
                            Console.WriteLine("Active: " + data.DeviceName + "-" + data.DeviceValue);
                            await _serviceDevice.ServiceDevice().Add(data.DeviceName, data.DeviceValue.ToString(), _engine);

                            // Console.WriteLine("MONITORING");
                        }
                        //if (target)
                        //{
                        //    Console.WriteLine($"High value:{data.DeviceName} {data.DeviceValue}");
                        //    //continue;
                        //}
                    }
                }
                else
                {
                    Console.WriteLine("PLC connection lost | Reconnecting...");
                }


            } while (true);
        }
    }
}
