using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Department
{
    public decimal DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public decimal? NumberOfPeople { get; set; }

	[JsonIgnore]
	public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

	[JsonIgnore]
	public virtual ICollection<Consume> Consumes { get; set; } = new List<Consume>();

	[JsonIgnore]
	public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
