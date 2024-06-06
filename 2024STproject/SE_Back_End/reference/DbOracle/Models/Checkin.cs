using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Checkin
{
    public decimal CustomerId { get; set; }

    public decimal RoomId { get; set; }

    public DateTime InTime { get; set; }

    public DateTime? OutTime { get; set; }

	[JsonIgnore]
	public virtual Customer Customer { get; set; } = null!;

	[JsonIgnore]
	public virtual Room Room { get; set; } = null!;
}
