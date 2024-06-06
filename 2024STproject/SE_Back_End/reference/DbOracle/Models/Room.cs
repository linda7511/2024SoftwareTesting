using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Room
{
    public decimal RoomId { get; set; }

    public decimal? TypeId { get; set; }

    public decimal? RoomNum { get; set; }

    public string? RoomPhone { get; set; }

    public string? Status { get; set; }

	[JsonIgnore]
	public virtual ICollection<Checkin> Checkins { get; set; } = new List<Checkin>();

	[JsonIgnore]
	public virtual ICollection<Cleaning> Cleanings { get; set; } = new List<Cleaning>();

	[JsonIgnore]
	public virtual ICollection<ConsumptionRecord> ConsumptionRecords { get; set; } = new List<ConsumptionRecord>();

	[JsonIgnore]
	public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

	[JsonIgnore]
	public virtual ICollection<Maintain> Maintains { get; set; } = new List<Maintain>();

	[JsonIgnore]
	public virtual Roomtype? Type { get; set; }
}
