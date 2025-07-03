using System;
using System.Collections.Generic;

namespace ProgramPLC.Models;

public partial class MonitoringEngine
{
    public int MonitorId { get; set; }

    public decimal HertzValue { get; set; }

    public decimal VoltsValue { get; set; }

    public decimal AmperValue { get; set; }

    public int? InitMonitorId { get; set; }

    public DateTime? Date { get; set; }

    public virtual InitMonitor? InitMonitor { get; set; }
}
