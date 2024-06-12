using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class LoanRequest
{
    public int RequestId { get; set; }

    public int? LoanId { get; set; }

    public int? CustId { get; set; }

    public DateTime? LoanRequestDateTime { get; set; }

    public string? LoanPurpose { get; set; }

    public decimal? RequestedAmount { get; set; }

    public int? RepaymentFrequency { get; set; }

    public string? RequestDetails { get; set; }

    public bool? DocumentUploadedStatus { get; set; }

    public bool? RequestStatus { get; set; }

    [JsonIgnore]
    public virtual Customer? Cust { get; set; }

    [JsonIgnore]
    public virtual LoanType? Loan { get; set; }

    [JsonIgnore]
    public virtual ICollection<LoanVerification> LoanVerifications { get; set; } = new List<LoanVerification>();
}
