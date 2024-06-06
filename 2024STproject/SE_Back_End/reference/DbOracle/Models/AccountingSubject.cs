using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class AccountingSubject
{
    public decimal AccountSubjectId { get; set; }

    public decimal? EmpId { get; set; }

    public string? Category { get; set; }

    public string? Debit { get; set; }

    public string? Credit { get; set; }

    public decimal? Amount { get; set; }

	[JsonIgnore]
	public virtual ICollection<AssetsInformation> AssetsInformations { get; set; } = new List<AssetsInformation>();

	[JsonIgnore]
	public virtual ICollection<AssetsSummarize> AssetsSummarizes { get; set; } = new List<AssetsSummarize>();

	[JsonIgnore]
	public virtual Employee? Emp { get; set; }
}
