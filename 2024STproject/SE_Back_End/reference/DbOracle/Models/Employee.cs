using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbOracle.Models;

public partial class Employee
{
    public decimal EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? Account { get; set; }

    public string? Sex { get; set; }

    public byte? Age { get; set; }

    public decimal? PostId { get; set; }

    public DateTime? EntryTime { get; set; }

    public string? PhoneNumber { get; set; }

    public decimal? BasePay { get; set; }

    public string? Password { get; set; }

    public string? BankName { get; set; }

    public string? CreditCardNumber { get; set; }

	[JsonIgnore]
	public virtual ICollection<AccountingSubject> AccountingSubjects { get; set; } = new List<AccountingSubject>();

	[JsonIgnore]
	public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

	[JsonIgnore]
	public virtual ICollection<AttendanceInformation> AttendanceInformations { get; set; } = new List<AttendanceInformation>();

	[JsonIgnore]
	public virtual ICollection<CheckInCheckOut> CheckInCheckOuts { get; set; } = new List<CheckInCheckOut>();

	[JsonIgnore]
	public virtual ICollection<Checkrepair> Checkrepairs { get; set; } = new List<Checkrepair>();

	[JsonIgnore]
	public virtual ICollection<Cleaning> Cleanings { get; set; } = new List<Cleaning>();

	[JsonIgnore]
	public virtual ICollection<ConsumptionRecord> ConsumptionRecords { get; set; } = new List<ConsumptionRecord>();

	[JsonIgnore]
	public virtual ICollection<Maintain> Maintains { get; set; } = new List<Maintain>();

	[JsonIgnore]
	public virtual Post? Post { get; set; }

	[JsonIgnore]
	public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

	[JsonIgnore]
	public virtual ICollection<Roomorder> Roomorders { get; set; } = new List<Roomorder>();

	[JsonIgnore]
	public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

	[JsonIgnore]
	public virtual ICollection<Scrap> Scraps { get; set; } = new List<Scrap>();

	[JsonIgnore]
	public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
