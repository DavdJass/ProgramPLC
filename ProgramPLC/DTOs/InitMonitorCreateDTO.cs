using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPLC.DTOs
{
    public class InitMonitorCreateDTO
    {
        public string Engine { get; set; } = null!;

        public string Hertz { get; set; } = null!;

        public string Volts { get; set; } = null!;

        public string Amper { get; set; } = null!;
        public string? HertzSetup { get; set; }
    }
}
