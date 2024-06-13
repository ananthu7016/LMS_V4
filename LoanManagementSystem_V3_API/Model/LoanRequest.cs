using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class LoanRequest
{
    public int RequestId { get; set; }

    public int? LoanTypeId { get; set; }

    public int? CustId { get; set; }

    public DateTime? LoanRequestDateTime { get; set; }

    public string? LoanPurpose { get; set; }

    public decimal? RequestedAmount { get; set; }

    public string? RequestDetails { get; set; }

    public bool? IsDocumentUploaded { get; set; }

    public bool? IsActive { get; set; }

    [JsonIgnore]
    public virtual Customer? Cust { get; set; }

    [JsonIgnore]
    public virtual LoanType? LoanType { get; set; }

    [JsonIgnore]
    public virtual ICollection<LoanVerificationSummary> LoanVerificationSummaries { get; set; } = new List<LoanVerificationSummary>();

    [JsonIgnore]
    public virtual ICollection<LoanVerification> LoanVerifications { get; set; } = new List<LoanVerification>();
}
