using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Customer
{
    public decimal CustomerId { get; set; }

    public string? Name { get; set; }

    public string? Id { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? CreditGrade { get; set; }

    public string? MemberGrade { get; set; }

	[JsonIgnore]
	public virtual ICollection<Book> Books { get; set; } = new List<Book>();

	[JsonIgnore]
	public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

	[JsonIgnore]
	public virtual ICollection<Checkin> Checkins { get; set; } = new List<Checkin>();

	[JsonIgnore]
	public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

	[JsonIgnore]
	public virtual ICollection<Pay> Pays { get; set; } = new List<Pay>();

	[JsonIgnore]
	public virtual ICollection<Roomorder> Roomorders { get; set; } = new List<Roomorder>();

	[JsonIgnore]
	public virtual ICollection<Service> Services { get; set; } = new List<Service>();

	[JsonIgnore]
	public virtual ICollection<Staging> Stagings { get; set; } = new List<Staging>();
}
