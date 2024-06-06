using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class EnterExit
{
    public decimal InfoId { get; set; }

    public string? CarNumber { get; set; }

    public DateTime? EnterTime { get; set; }

    public DateTime? ExitTime { get; set; }

	[JsonIgnore]
	public virtual Car? CarNumberNavigation { get; set; }
}
