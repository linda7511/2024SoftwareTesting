using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Bill
{
    public decimal BillId { get; set; }

    public decimal? RoomCost { get; set; }

    public decimal? FoodCost { get; set; }

    public decimal? OtherCost { get; set; }

    public string? BillType { get; set; }

    public string? InvoiceNumber { get; set; }

	[JsonIgnore]
	public virtual ICollection<ConsumptionRecord> ConsumptionRecords { get; set; } = new List<ConsumptionRecord>();

	[JsonIgnore]
	public virtual ICollection<Pay> Pays { get; set; } = new List<Pay>();
}
