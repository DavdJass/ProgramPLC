using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPLC.DTOs
{
    public class InitMonitorReadDTO
    {
        public int InitMonitorId { get; set; }

        public string Hertz { get; set; }

        public string Volts { get; set; } 

        public string Amper { get; set; }
    }
}
