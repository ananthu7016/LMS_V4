using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class LoanVerificationSummary
{
    public int SummaryId { get; set; }

    public int? RequestId { get; set; }

    public string? Summary { get; set; }

    public DateTime? StatusChangedDateTime { get; set; }

    [JsonIgnore]
    public virtual LoanRequest? Request { get; set; }
}
