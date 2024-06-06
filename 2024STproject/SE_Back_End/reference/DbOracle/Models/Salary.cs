using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Salary
{
    public decimal SalaryId { get; set; }

    public decimal? EmpId { get; set; }

    public decimal? Year { get; set; }

    public decimal? Month { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? HolidayAllowance { get; set; }

    public decimal? OtherAllowance { get; set; }

    public decimal? Commission { get; set; }

    public decimal? YearEndBonus { get; set; }

    public decimal? OvertimePay { get; set; }

    public string? RewardType { get; set; }

    public decimal? RewardAmount { get; set; }

    public decimal? LateDeduction { get; set; }

    public decimal? EarlyDepartureDeduction { get; set; }

    public decimal? IncomeTax { get; set; }

    public decimal? SocialInsurance { get; set; }

    public decimal? GrossSalary { get; set; }

    public decimal? NetSalary { get; set; }

	[JsonIgnore]
	public virtual Employee? Emp { get; set; }
}
