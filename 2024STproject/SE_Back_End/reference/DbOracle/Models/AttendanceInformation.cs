using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class AttendanceInformation
{
    public decimal AttendanceId { get; set; }

    public decimal? EmpId { get; set; }

    public decimal? Year { get; set; }

    public decimal? Month { get; set; }

    public decimal? ExpectAttendanceDays { get; set; }

    public decimal? ActualAttendanceDays { get; set; }

    public decimal? PersonalLeaveDays { get; set; }

    public decimal? SickLeaveDays { get; set; }

    public decimal? LateDays { get; set; }

    public decimal? EarlyDepartureDays { get; set; }

    public decimal? AbsentDays { get; set; }

	[JsonIgnore]
	public virtual Employee? Emp { get; set; }
}
