using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class UploadedDocument
{
    public int UploadId { get; set; }

    public int? CustId { get; set; }

    public int? DocTypeId { get; set; }

    public string? DocPath { get; set; }

    [JsonIgnore]
    public virtual Customer? Cust { get; set; }

    [JsonIgnore]
    public virtual DocumentType? DocType { get; set; }
}
