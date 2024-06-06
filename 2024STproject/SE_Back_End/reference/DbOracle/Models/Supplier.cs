using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Supplier
{
    public decimal SupplierId { get; set; }

    public string? SupplierPhone { get; set; }

    public string? Address { get; set; }

    [JsonIgnore]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
