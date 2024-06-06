using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Mytable
{
    public decimal TableId { get; set; }

    public decimal? CapacityNum { get; set; }

    public string? TableType { get; set; }

    public string? TableLocation { get; set; }

    public string? TableStatus { get; set; }

    public string? Note { get; set; }

    public decimal? Bookable { get; set; }

    public decimal? Available { get; set; }

	[JsonIgnore]
	public virtual ICollection<Book> Books { get; set; } = new List<Book>();

	[JsonIgnore]
	public virtual ICollection<Myorder> Myorders { get; set; } = new List<Myorder>();
}
