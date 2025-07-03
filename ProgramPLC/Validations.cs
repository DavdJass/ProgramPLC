using ProgramPLC.Models;
using ProgramPLC.Services;

namespace ProgramPLC
{
    public class Validations
    {
        public static Dictionary<string, string> variables = new Dictionary<string, string>();
        public static bool ValHertzSetup(ICommonService<InitMonitor> serviceMonitor, ICommonService<Device> serviceDevice,
            string deviceName, string deviceValue)
        {
            var current = serviceMonitor.GetHzSetup(deviceName);

            try
            {
                if (current.Result != null)
                {
                    var plc = new PLCservice();
                    var variable = plc.ReadData(current.Result.HertzSetup);
                    if (variable == null)
                        return false;

                    //Console.WriteLine(deviceName + "-" + deviceValue + "-" + variable.DeviceValue);

                    if (double.Parse(variable.DeviceValue) == 0)
                        return false;
                    if (double.Parse(deviceValue) == 0)
                        return false;
                    //NEW CONDITION
                    //if (double.Parse(deviceValue) >= (double.Parse(variable.DeviceValue) / 100))
                    //{
                    //    return ValDual(deviceName, deviceValue);
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                    return double.Parse(deviceValue) >= (double.Parse(variable.DeviceValue) / 100);
                    //}
                }
                else

                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("err "+ex.Message);
                return false;
            }
           

        }
        public static void UpdateStatusEngine(ICommonService<InitMonitor> serviceMonitor, string data)
        {
            serviceMonitor.Update(data);
        }
        public static void UpdateStatusFalseEngine(ICommonService<InitMonitor> serviceMonitor, string data)
        {
            serviceMonitor.UpdateFalse(data);
        }

        public static bool IsActived(ICommonService<InitMonitor> serviceMonitor, string data)
        {
            var device = serviceMonitor.GetOneDevice(data);
            
            if (device.Result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ValDual(string deviceName, string deviceValue)
        {
            //NEW CONDITION

                if (!variables.ContainsKey(deviceName))
                {
                    variables.Add(deviceName, deviceValue);
                    //Console.WriteLine("Add dictionary " + deviceName + "-" + deviceValue);
                }
                if (variables.ContainsKey(deviceName) && (variables[deviceName] != deviceValue))
                {
                    variables[deviceName] = deviceValue;
                    //Console.WriteLine("Change dictionary " + deviceName + "-" + deviceValue);
                    return true;
                }
                else
                {
                    return false;
                }
            
        }
        public static bool OnTarget(ICommonService<InitMonitor> serviceMonitor,string deviceName, string deviceValue)
        {
            var result= serviceMonitor.GetOneDevice(deviceName);
            if (result.Result != null && deviceName == result.Result.Amper)
            {
                if(decimal.Parse(deviceValue) >= result.Result.MotorPlateData)
                {   
                    PLCservice.WriteData(result.Result.Signal, 1);
                    return true;
                }
                else
                {
                    PLCservice.WriteData(result.Result.Signal, 0);
                }
            }
            return false;
        }
    }
}
