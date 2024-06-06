using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Application
{
    public decimal ApplicationId { get; set; }

    public DateTime? ApplicationTime { get; set; }

    public string? ApplicationType { get; set; }

    public string? ApplicationContent { get; set; }

    public string? IfApprove { get; set; }

    public decimal? EmployeeId { get; set; }

	[JsonIgnore]
	public virtual Employee? Employee { get; set; }
}
