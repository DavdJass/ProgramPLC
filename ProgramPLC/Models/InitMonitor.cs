using System;
using System.Collections.Generic;

namespace ProgramPLC.Models;

public partial class InitMonitor
{
    public int InitMonitorId { get; set; }

    public string Engine { get; set; } = null!;

    public string Hertz { get; set; } = null!;

    public string Volts { get; set; } = null!;

    public string Amper { get; set; } = null!;

    public bool StatusM { get; set; }

    public DateTime? Date { get; set; }

    public string? HertzSetup { get; set; }
    public decimal? MotorPlateData { get; set; }
    public string? Signal { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual ICollection<MonitoringEngine> MonitoringEngines { get; set; } = new List<MonitoringEngine>();
}
