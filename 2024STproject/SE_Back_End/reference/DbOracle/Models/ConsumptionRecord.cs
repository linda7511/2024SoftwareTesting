using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class ConsumptionRecord
{
    public decimal ConsumeId { get; set; }

    public decimal? RoomId { get; set; }

    public decimal? BillId { get; set; }

    public decimal? EmployeeId { get; set; }

    public string? ConsumeType { get; set; }

    public DateTime? ConsumeTime { get; set; }

    public decimal? ConsumeAmount { get; set; }

	[JsonIgnore]
	public virtual Bill? Bill { get; set; }

	[JsonIgnore]
	public virtual Employee? Employee { get; set; }

	[JsonIgnore]
	public virtual Room? Room { get; set; }

    [JsonIgnore]
    public virtual ICollection<Myorder> Myorders { get; set; } = new List<Myorder>();
}
