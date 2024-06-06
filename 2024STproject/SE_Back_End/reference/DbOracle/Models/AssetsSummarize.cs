using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class AssetsSummarize
{
    public decimal AssetsSummarizeId { get; set; }

    public decimal? AccountSubjectId { get; set; }

    public long? AssetsAmount { get; set; }

    public long? AssetsDepreciation { get; set; }

    public long? AssetPresentValue { get; set; }

    public decimal? ResponsPersonId { get; set; }

	[JsonIgnore]
	public virtual AccountingSubject? AccountSubject { get; set; }
}
