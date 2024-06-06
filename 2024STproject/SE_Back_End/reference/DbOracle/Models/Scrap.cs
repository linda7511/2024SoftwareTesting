using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Scrap
{
    public decimal EmpId { get; set; }

    public decimal AssetId { get; set; }

    public DateTime? ScrapTime { get; set; }

    public string? ScrapReason { get; set; }

	[JsonIgnore]
	public virtual AssetsInformation Asset { get; set; } = null!;

	[JsonIgnore]
	public virtual Employee Emp { get; set; } = null!;
}
