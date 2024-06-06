using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class AssetsInformation
{
    public decimal AssetId { get; set; }

    public decimal? AccountSubjectId { get; set; }

    public string? AssetsName { get; set; }

    public string? AssetsCategory { get; set; }

    public decimal? LocationId { get; set; }

    public DateTime? RegistrationTime { get; set; }

    public long? OriginalAssetsAmount { get; set; }

    public string? Valid { get; set; }

	[JsonIgnore]
	public virtual AccountingSubject? AccountSubject { get; set; }

	[JsonIgnore]
	public virtual ICollection<Checkrepair> Checkrepairs { get; set; } = new List<Checkrepair>();

	[JsonIgnore]
	public virtual Location? Location { get; set; }

	[JsonIgnore]
	public virtual ICollection<Scrap> Scraps { get; set; } = new List<Scrap>();
}
