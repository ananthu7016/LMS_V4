using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public bool? IsActive { get; set; }

    [JsonIgnore]
    public virtual ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();

    [JsonIgnore]
    public virtual ICollection<LoanVerification> LoanVerifications { get; set; } = new List<LoanVerification>();

    [JsonIgnore]
    public virtual Role? Role { get; set; }
}
