using System;
using System.Collections.Generic;

namespace ProgramPLC.Models;

public partial class Device
{
    public int DeviceId { get; set; }

    public string DeviceName { get; set; } = null!;

    public string DeviceValue { get; set; } = null!;

    public int EngineId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual InitMonitor Engine { get; set; } = null!;
}
