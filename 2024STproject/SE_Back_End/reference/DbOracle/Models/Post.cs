using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Post
{
    public decimal PostId { get; set; }

    public string? PostName { get; set; }

    public decimal? DepartmentId { get; set; }

    public string? Authority { get; set; }

    public string? PositionLevel { get; set; }

    public decimal? PositionSalary { get; set; }

    public string? PaymentType { get; set; }

    public string? PaymentBase { get; set; }

    public string? Insurance { get; set; }

    public decimal? AccumulationFunds { get; set; }

	[JsonIgnore]
	public virtual Department? Department { get; set; }

	[JsonIgnore]
	public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
