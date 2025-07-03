using System;
using System.Collections.Generic;

namespace ProgramPLC.Models;

public partial class Engine
{
    public int EngineId { get; set; }

    public string? EngineName { get; set; }

    public string? EngineDescription { get; set; }

    public DateTime? CreatedDate { get; set; }
}
