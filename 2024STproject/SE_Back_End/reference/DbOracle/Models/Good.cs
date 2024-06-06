using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Good
{
    public decimal GoodsId { get; set; }

    public string? Category { get; set; }

    public string? GoodsName { get; set; }

    public decimal? Inventory { get; set; }

	[JsonIgnore]
	public virtual ICollection<Consume> Consumes { get; set; } = new List<Consume>();

	[JsonIgnore]
	public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();


}
