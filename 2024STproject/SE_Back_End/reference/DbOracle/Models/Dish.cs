using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Dish
{
    public decimal DishId { get; set; }

    public string? DishName { get; set; }

    public decimal? DishPrice { get; set; }

    public string? DishTaste { get; set; }

	[JsonIgnore]
	public virtual ICollection<Myorder> Myorders { get; set; } = new List<Myorder>();
}
