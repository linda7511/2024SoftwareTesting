using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Assignment
{
    public decimal AssignmentId { get; set; }

    public decimal? DepartmentId { get; set; }

    public string? AssignmentName { get; set; }

    public string? AssignmentNote { get; set; }

	[JsonIgnore]
	public virtual Department? Department { get; set; }
}
