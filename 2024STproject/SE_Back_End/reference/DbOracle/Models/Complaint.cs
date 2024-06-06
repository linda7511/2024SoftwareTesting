using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Complaint
{
    public decimal ComplaintId { get; set; }

    public decimal? CustomerId { get; set; }

    public string? Way { get; set; }

    public DateTime? SubmitTime { get; set; }

    public string? ComplaintType { get; set; }

    public string? Description { get; set; }

    public string? Feedback { get; set; }

    public string? Result { get; set; }

	[JsonIgnore]
	public virtual Customer? Customer { get; set; }
}
