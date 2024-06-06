using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Cleaning
{
    public decimal RoomId { get; set; }

    public decimal EmpId { get; set; }

    public DateTime CleaningTime { get; set; }

	[JsonIgnore]
	public virtual Employee Emp { get; set; } = null!;

	[JsonIgnore]
	public virtual Room Room { get; set; } = null!;
}
