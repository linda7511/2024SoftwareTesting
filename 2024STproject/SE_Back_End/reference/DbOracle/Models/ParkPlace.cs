using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class ParkPlace
{
    public decimal ParkingSpaceId { get; set; }

    public string? Type { get; set; }

    public string? Area { get; set; }

    public string? Status { get; set; }

	[JsonIgnore]
	public virtual ICollection<Parking> Parkings { get; set; } = new List<Parking>();

	[JsonIgnore]
	public virtual UnitPrice? TypeNavigation { get; set; }
}
