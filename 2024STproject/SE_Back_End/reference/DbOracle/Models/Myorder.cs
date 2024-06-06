using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Myorder
{
    public decimal TableId { get; set; }

    public decimal DishId { get; set; }

    public DateTime OrderTime { get; set; }

    public decimal? ConsumptionRecordId { get; set; }

    public string? OrderStatus { get; set; }

	[JsonIgnore]
	public virtual ConsumptionRecord? ConsumptionRecord { get; set; }

	[JsonIgnore]
	public virtual Dish Dish { get; set; } = null!;

    public virtual Mytable Table { get; set; } = null!;
}
