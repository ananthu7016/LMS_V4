using System;
using System.Collections.Generic;

namespace LoanManagementSystem_V3_API.Model;

public partial class Log
{
    public int LogId { get; set; }

    public string? EventType { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? LogDescription { get; set; }

    public bool? LogStatus { get; set; }
}
