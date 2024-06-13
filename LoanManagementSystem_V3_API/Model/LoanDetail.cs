using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class LoanDetail
{
    public int DetailId { get; set; }

    public int? LoanTypeId { get; set; }

    public int? CustId { get; set; }

    public decimal? LoanAmount { get; set; }

    public DateTime? LoanRequestDateTime { get; set; }

    public DateTime? LoanSanctionDateTime { get; set; }

    public string? LoanPurpose { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }

    [JsonIgnore]
    public virtual Customer? Cust { get; set; }

    [JsonIgnore]
    public virtual LoanType? LoanType { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}
