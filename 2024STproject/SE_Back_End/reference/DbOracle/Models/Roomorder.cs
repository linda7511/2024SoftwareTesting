using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Roomorder
{
    public decimal OrderId { get; set; }

    public decimal? CustomerId { get; set; }

    public decimal? TypeId { get; set; }

    public decimal? EmpId { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? CreateTime { get; set; }

    public decimal? Num { get; set; }

    public DateTime? ExpectInTime { get; set; }

    public DateTime? ExpectOutTime { get; set; }

    public decimal? Price { get; set; }

    public string? Note { get; set; }

	[JsonIgnore]
	public virtual Customer? Customer { get; set; }

	[JsonIgnore]
	public virtual Employee? Emp { get; set; }

	[JsonIgnore]
	public virtual Roomtype? Type { get; set; }
}
