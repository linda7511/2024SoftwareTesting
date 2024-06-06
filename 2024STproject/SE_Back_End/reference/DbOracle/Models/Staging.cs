using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Staging
{
    public decimal LuggageId { get; set; }

    public decimal? CustomerId { get; set; }

    public DateTime? StagingTime { get; set; }

    public DateTime? FetchTime { get; set; }

    public string? LuggageType { get; set; }

	[JsonIgnore]
	public virtual Customer? Customer { get; set; }
}
