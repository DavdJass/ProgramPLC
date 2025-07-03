using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPLC.DTOs
{
    public class DeviceReadDTO
    {
        public int? DeviceId { get; set; }

        public string? DeviceName { get; set; } = null!;

        public string? DeviceValue { get; set; } = null!;

        public int? EngineId { get; set; }

    }
}
