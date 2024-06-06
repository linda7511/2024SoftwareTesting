using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Supply
{
    public decimal SupplierId { get; set; }

    public decimal GoodsId { get; set; }

    public decimal? Price { get; set; }

    public decimal? SurplusQuantity { get; set; }

	[JsonIgnore]
	public virtual Good Goods { get; set; } = null!;

	[JsonIgnore]
	public virtual Supplier Supplier { get; set; } = null!;
}
