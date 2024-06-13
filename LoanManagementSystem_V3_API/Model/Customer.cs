using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FullName { get; set; }

    public string? Occupation { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Aadhar { get; set; }

    public DateTime? Dob { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool? CustEmploymentStatus { get; set; }

    public DateTime? RegisteredDateTime { get; set; }

    public bool? IsActive { get; set; }

    [JsonIgnore]
    public virtual ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();

    [JsonIgnore]
    public virtual ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();
}
