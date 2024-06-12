using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class LoanVerification
{
    public int VerificationId { get; set; }

    public int? RequestId { get; set; }

    public int? VerifiedBy { get; set; }

    public string? VerificationReview { get; set; }

    public bool? VerificationStatus { get; set; }

    public DateTime? StatusChangedDateTime { get; set; }

    [JsonIgnore]
    public virtual LoanRequest? Request { get; set; }

    [JsonIgnore]
    public virtual User? VerifiedByNavigation { get; set; }
}
