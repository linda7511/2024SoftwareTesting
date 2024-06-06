using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Consume
{
    public decimal DepartmentId { get; set; }

    public decimal GoodsId { get; set; }

    public decimal? ConsumeQuantity { get; set; }

	[JsonIgnore]
	public virtual Department Department { get; set; } = null!;

	[JsonIgnore]
	public virtual Good Goods { get; set; } = null!;
}
