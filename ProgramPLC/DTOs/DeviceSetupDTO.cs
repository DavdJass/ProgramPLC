using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPLC.DTOs
{
    public class DeviceSetupDTO
    {
        public int initMonitorID { get; set; }
        public string? Engine { get; set; }
        public string? Hertz {  get; set; }
        public string? Volts {  get; set; }
        public string? Amper {  get; set; }
        public string? HertzSetup {  get; set; }
    }
}
