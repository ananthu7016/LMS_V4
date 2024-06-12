using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class LoanType
{
    public int LoanId { get; set; }

    public string? LoanName { get; set; }

    public string? LoanDescription { get; set; }

    public decimal? LoanMinimumAmount { get; set; }

    public decimal? LoanMaximumAmount { get; set; }

    public decimal? LoanIntrestRate { get; set; }

    public decimal? LatePaymentPenalty { get; set; }

    public decimal? ProcessingFee { get; set; }

    public decimal? TaxPercentage { get; set; }

    public int? RepaymentFrequency { get; set; }

    public int? GracePeriod { get; set; }

    public bool? EmployementStatusRequired { get; set; }

    public bool? CollateralRequired { get; set; }

    public int? LoanTerm { get; set; }

    [JsonIgnore]
    public DateTime? CreatedDateTime { get; set; }

    [JsonIgnore]
    public bool? LoanStatus { get; set; }

    [JsonIgnore]
    public virtual ICollection<LoanDeatil> LoanDeatils { get; set; } = new List<LoanDeatil>();

    [JsonIgnore]
    public virtual ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();
}
