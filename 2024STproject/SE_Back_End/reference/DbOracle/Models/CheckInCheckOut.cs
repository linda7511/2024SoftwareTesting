using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class CheckInCheckOut
{
    public decimal CheckInId { get; set; }

    public decimal? EmpId { get; set; }

    public DateTime? CheckInDate { get; set; }

	[JsonIgnore]
	public virtual Employee? Emp { get; set; }
}
