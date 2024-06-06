using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class UnitPrice
{
    public string ParkingPlaceType { get; set; } = null!;

    public decimal? MemberPrice { get; set; }

    public decimal? NotMemberPrice { get; set; }

	[JsonIgnore]
	public virtual ICollection<ParkPlace> ParkPlaces { get; set; } = new List<ParkPlace>();
}
