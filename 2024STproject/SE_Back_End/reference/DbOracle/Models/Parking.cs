using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Parking
{
    public decimal ParkingSpaceId { get; set; }

    public string CarNumber { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

	[JsonIgnore]
	public virtual Car CarNumberNavigation { get; set; } = null!;

	[JsonIgnore]
	public virtual ParkPlace ParkingSpace { get; set; } = null!;
}
