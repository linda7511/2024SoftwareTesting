using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Purchase
{
	public decimal PurchaseId { get; set; }
	public decimal GoodsId { get; set; }

    public decimal EmployeeId { get; set; }

	public decimal SupplierId { get; set; }

	public DateTime? PurchaseDate { get; set; }

	public decimal PurchaseQuantity { get; set; }

	public decimal UnitPrice { get; set; }


	[JsonIgnore]
	public virtual Employee Employee { get; set; } = null!;

	[JsonIgnore]
	public virtual Good Goods { get; set; } = null!;

	[JsonIgnore]
	public virtual Supplier Suppliers { get; set; } = null!;

}
