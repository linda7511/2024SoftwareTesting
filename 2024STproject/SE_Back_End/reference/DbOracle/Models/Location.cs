using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Location
{
    public decimal LocationId { get; set; }

    public decimal? Floor { get; set; }

    public decimal? RoomId { get; set; }

    public string? Note { get; set; }

	[JsonIgnore]
	public virtual ICollection<AssetsInformation> AssetsInformations { get; set; } = new List<AssetsInformation>();

	[JsonIgnore]
	public virtual Room? Room { get; set; }
}
