using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramPLC.Models;

namespace ProgramPLC.DTOs
{
    public class DevicesDTO
    {
        public Device Hertz {  get; set; }
        public Device Volts { get; set; }
        public Device Amper { get; set; }

    }
}
