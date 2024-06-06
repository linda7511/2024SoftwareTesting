using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Service
{
    public decimal CustomerId { get; set; }

    public decimal EmpId { get; set; }

    public DateTime ServiceTime { get; set; }

    public string? ServiceType { get; set; }

    public string? Result { get; set; }

	[JsonIgnore]
	public virtual Customer Customer { get; set; } = null!;

	[JsonIgnore]
	public virtual Employee Emp { get; set; } = null!;
}
