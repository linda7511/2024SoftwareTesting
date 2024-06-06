using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Pay
{
    public decimal CustomerId { get; set; }

    public decimal BillId { get; set; }

    public string? PayWay { get; set; }

    public DateTime? PayTime { get; set; }

    public decimal? PayCount { get; set; }

    public string? Status { get; set; }

	[JsonIgnore]
	public virtual Bill Bill { get; set; } = null!;

	[JsonIgnore]
	public virtual Customer Customer { get; set; } = null!;
}
