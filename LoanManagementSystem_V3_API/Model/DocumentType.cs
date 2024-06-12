using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanManagementSystem_V3_API.Model;

public partial class DocumentType
{
    public int DocTypeId { get; set; }

    public string? DocTypeName { get; set; }

    [JsonIgnore]
    public virtual ICollection<UploadedDocument> UploadedDocuments { get; set; } = new List<UploadedDocument>();
}
