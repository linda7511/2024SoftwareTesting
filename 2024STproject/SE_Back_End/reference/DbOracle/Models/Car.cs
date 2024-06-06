using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Car
{
    public string CarNumber { get; set; } = null!;

    public decimal? CustomerId { get; set; }

	[JsonIgnore]
	public virtual Customer? Customer { get; set; }

	[JsonIgnore]
	public virtual ICollection<EnterExit> EnterExits { get; set; } = new List<EnterExit>();

	[JsonIgnore]
	public virtual ICollection<Parking> Parkings { get; set; } = new List<Parking>();
}
