using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class LoanType
{
    public int LoanTypeId { get; set; }

    public string? LoanTypeName { get; set; }

    public string? LoanDescription { get; set; }

    public decimal? LoanMinimumAmount { get; set; }

    public decimal? LoanMaximumAmount { get; set; }

    public decimal? LoanInterestRate { get; set; }

    public decimal? ProcessingFee { get; set; }

    public decimal? TaxPercentage { get; set; }

    public bool? IsEmploymentStatusRequired { get; set; }

    public int? LoanTerm { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public bool? IsActive { get; set; }

    [JsonIgnore]
    public virtual ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();

    [JsonIgnore]
    public virtual ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();
}
