using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Book
{
    public decimal TableId { get; set; }

    public decimal CustomerId { get; set; }

    public DateTime BookTime { get; set; }

    public decimal? Num { get; set; }

    public string? BookStatus { get; set; }

    public string? SpecialDemand { get; set; }

    public string? Note { get; set; }

	[JsonIgnore]
	public virtual Customer Customer { get; set; } = null!;

	[JsonIgnore]
	public virtual Mytable Table { get; set; } = null!;
}
