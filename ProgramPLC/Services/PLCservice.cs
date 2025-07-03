using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//susing ActUtlType64Lib;
//using ActProgType64Lib;
using ActUtlTypeLib;

using ProgramPLC.DTOs;

namespace ProgramPLC.Services
{
    public class PLCservice
    {
        private static ActUtlType plc;
       // private static ActProgType64 plc;
       // private static bool _connection = false;
        private static bool _openC = false;

        public static bool ConnectionPLC(int lgNumber)
        {
           plc = new ActUtlType();
           plc.ActLogicalStationNumber = lgNumber;
            //plc = new ActProgType64();


            //// Configure PLC communication settings
            //plc.ActHostAddress = "192.168.1.100"; // PLC IP address
            //plc.ActPortNumber = 5000;             // Port number (check PLC settings)
            //plc.ActCpuType = 0x01A1;              // CPU type (e.g., Q03UDV, refer to MX Component manual)
            //plc.ActNetworkNumber = 0;             // Network number (usually 0 for direct connection)
            //plc.ActStationNumber = 255;           // Station number (255 for direct Ethernet)
            //plc.ActUnitNumber = 0;                // Unit number (usually 0)
            //plc.ActProtocolType = 0x05;           // Protocol: 0x05 for TCP/IP (refer to manual)
            //plc.ActTimeOut = 10000;               // Timeout in milliseconds


            try
            {
                plc.Open();
               // getSatet();
                plc.GetCpuType(out string szCpuName, out int iCpuType);
                Console.WriteLine($"Conexion al PLC:{szCpuName} CPU TYPE: {iCpuType}");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al conectar al PLC "+e.ToString());
                //_connection = false;
                return false;
            }
            
        }
        public static bool getSatet()
        {
            //CHECK THIS
           // plc.Open();
            plc.GetCpuType(out string szCpuName, out int iCpuType);
           // Console.WriteLine(iCpuType);

            if (iCpuType != 0)
            {
                if (_openC == false)
                {
                    Console.WriteLine($"Conexion al PLC:{szCpuName} CPU TYPE: {iCpuType}");
                   // _connection = true;
                    _openC = true;
                    return true;
                }
                else
                {
                    _openC = true;
                    return true;
                }
               
            }
            else
            {
                //_connection = false;
                _openC= false;
                return false;
            }
            //return _connection;
        }

        public static void CloseConnectionPLC()
        {
            plc.Close();
        }

        public PLCdto ReadData(string address)
        {
            PLCdto data = new PLCdto();
            //plc.Open();
            plc.GetDevice(address, out int value);
            data.DeviceName = address;
            data.DeviceValue = value.ToString();
            //plc.Close();

            return data;
        }
        public static void WriteData(string szDevice, int IData)
        {
            plc.SetDevice(szDevice, IData);
        }
    }

}

