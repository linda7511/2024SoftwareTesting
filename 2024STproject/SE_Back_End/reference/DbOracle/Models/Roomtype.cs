using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Roomtype
{
    public decimal TypeId { get; set; }

    public string? TypeName { get; set; }

    public decimal? Price { get; set; }

    public string? Note { get; set; }

    public decimal? Remaining { get; set; }

	[JsonIgnore]
	public virtual ICollection<Roomorder> Roomorders { get; set; } = new List<Roomorder>();

	[JsonIgnore]
	public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
