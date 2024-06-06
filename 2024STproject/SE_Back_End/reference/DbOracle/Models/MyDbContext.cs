using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbOracle.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
	{
		ChangeTracker.LazyLoadingEnabled = false;
	}

    public virtual DbSet<AccountingSubject> AccountingSubjects { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<AssetsInformation> AssetsInformations { get; set; }

    public virtual DbSet<AssetsSummarize> AssetsSummarizes { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AttendanceInformation> AttendanceInformations { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CheckInCheckOut> CheckInCheckOuts { get; set; }

    public virtual DbSet<Checkin> Checkins { get; set; }

    public virtual DbSet<Checkrepair> Checkrepairs { get; set; }

    public virtual DbSet<Cleaning> Cleanings { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Consume> Consumes { get; set; }

    public virtual DbSet<ConsumptionRecord> ConsumptionRecords { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EnterExit> EnterExits { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Maintain> Maintains { get; set; }

    public virtual DbSet<Myorder> Myorders { get; set; }

    public virtual DbSet<Mytable> Mytables { get; set; }

    public virtual DbSet<ParkPlace> ParkPlaces { get; set; }

    public virtual DbSet<Parking> Parkings { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Roomorder> Roomorders { get; set; }

    public virtual DbSet<Roomtype> Roomtypes { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Scrap> Scraps { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Staging> Stagings { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<UnitPrice> UnitPrices { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User ID=dbc_user;Password=dbc_user;");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("DESIGN")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AccountingSubject>(entity =>
        {
            entity.HasKey(e => e.AccountSubjectId).HasName("SYS_C008866");

            entity.ToTable("ACCOUNTING_SUBJECTS");

            entity.Property(e => e.AccountSubjectId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ACCOUNT_SUBJECT_ID");
            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Category)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.Credit)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("CREDIT");
            entity.Property(e => e.Debit)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("DEBIT");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");

            entity.HasOne(d => d.Emp).WithMany(p => p.AccountingSubjects)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("SYS_C008867");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("SYS_C008857");

            entity.ToTable("APPLICATION");

            entity.Property(e => e.ApplicationId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("APPLICATION_ID");
            entity.Property(e => e.ApplicationContent)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("APPLICATION_CONTENT");
            entity.Property(e => e.ApplicationTime)
                .HasColumnType("DATE")
                .HasColumnName("APPLICATION_TIME");
            entity.Property(e => e.ApplicationType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("APPLICATION_TYPE");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.IfApprove)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IF_APPROVE");

            entity.HasOne(d => d.Employee).WithMany(p => p.Applications)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("SYS_C008858");
        });

        modelBuilder.Entity<AssetsInformation>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("SYS_C008874");

            entity.ToTable("ASSETS_INFORMATION");

            entity.Property(e => e.AssetId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ASSET_ID");
            entity.Property(e => e.AccountSubjectId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ACCOUNT_SUBJECT_ID");
            entity.Property(e => e.AssetsCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ASSETS_CATEGORY");
            entity.Property(e => e.AssetsName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ASSETS_NAME");
            entity.Property(e => e.LocationId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LOCATION_ID");
            entity.Property(e => e.OriginalAssetsAmount)
                .HasPrecision(12)
                .HasColumnName("ORIGINAL_ASSETS_AMOUNT");
            entity.Property(e => e.RegistrationTime)
                .HasPrecision(6)
                .HasColumnName("REGISTRATION_TIME");
            entity.Property(e => e.Valid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("'Y'")
                .HasColumnName("VALID");

            entity.HasOne(d => d.AccountSubject).WithMany(p => p.AssetsInformations)
                .HasForeignKey(d => d.AccountSubjectId)
                .HasConstraintName("SYS_C008875");

            entity.HasOne(d => d.Location).WithMany(p => p.AssetsInformations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("SYS_C008876");
        });

        modelBuilder.Entity<AssetsSummarize>(entity =>
        {
            entity.HasKey(e => e.AssetsSummarizeId).HasName("SYS_C008877");

            entity.ToTable("ASSETS_SUMMARIZE");

            entity.Property(e => e.AssetsSummarizeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ASSET_ID");
            entity.Property(e => e.AccountSubjectId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ACCOUNT_SUBJECT_ID");
            entity.Property(e => e.AssetPresentValue)
                .HasPrecision(12)
                .HasColumnName("ASSET_PRESENT_VALUE");
            entity.Property(e => e.AssetsAmount)
                .HasPrecision(12)
                .HasColumnName("ASSETS_AMOUNT");
            entity.Property(e => e.AssetsDepreciation)
                .HasPrecision(12)
                .HasColumnName("ASSETS_DEPRECIATION");
            entity.Property(e => e.ResponsPersonId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RESPONS_PERSON_ID");

            entity.HasOne(d => d.AccountSubject).WithMany(p => p.AssetsSummarizes)
                .HasForeignKey(d => d.AccountSubjectId)
                .HasConstraintName("SYS_C008878");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("SYS_C008793");

            entity.ToTable("ASSIGNMENT");

            entity.Property(e => e.AssignmentId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ASSIGNMENT_ID");
            entity.Property(e => e.AssignmentName)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("ASSIGNMENT_NAME");
            entity.Property(e => e.AssignmentNote)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("ASSIGNMENT_NOTE");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DEPARTMENT_ID");

            entity.HasOne(d => d.Department).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("SYS_C008794");
        });

        modelBuilder.Entity<AttendanceInformation>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("SYS_C008862");

            entity.ToTable("ATTENDANCE_INFORMATION");

            entity.Property(e => e.AttendanceId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ATTENDANCE_ID");
            entity.Property(e => e.AbsentDays)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABSENT_DAYS");
            entity.Property(e => e.ActualAttendanceDays)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ACTUAL_ATTENDANCE_DAYS");
            entity.Property(e => e.EarlyDepartureDays)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EARLY_DEPARTURE_DAYS");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.ExpectAttendanceDays)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EXPECT_ATTENDANCE_DAYS");
            entity.Property(e => e.LateDays)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LATE_DAYS");
            entity.Property(e => e.Month)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MONTH");
            entity.Property(e => e.PersonalLeaveDays)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PERSONAL_LEAVE_DAYS");
            entity.Property(e => e.SickLeaveDays)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SICK_LEAVE_DAYS");
            entity.Property(e => e.Year)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("YEAR");

            entity.HasOne(d => d.Emp).WithMany(p => p.AttendanceInformations)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("SYS_C008863");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("SYS_C008804");

            entity.ToTable("BILL");

            entity.Property(e => e.BillId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BILL_ID");
            entity.Property(e => e.BillType)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("BILL_TYPE");
            entity.Property(e => e.FoodCost)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("FOOD_COST");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("INVOICE_NUMBER");
            entity.Property(e => e.OtherCost)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("OTHER_COST");
            entity.Property(e => e.RoomCost)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("ROOM_COST");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => new { e.TableId, e.CustomerId, e.BookTime }).HasName("SYS_C008814");

            entity.ToTable("BOOK");

            entity.Property(e => e.TableId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TABLE_ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.BookTime)
                .HasPrecision(6)
                .HasColumnName("BOOK_TIME");
            entity.Property(e => e.BookStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BOOK_STATUS");
            entity.Property(e => e.Note)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.Num)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NUM");
            entity.Property(e => e.SpecialDemand)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("SPECIAL_DEMAND");

            entity.HasOne(d => d.Customer).WithMany(p => p.Books)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008816");

            entity.HasOne(d => d.Table).WithMany(p => p.Books)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008815");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarNumber).HasName("SYS_C008828");

            entity.ToTable("CAR");

            entity.Property(e => e.CarNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CAR_NUMBER");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("SYS_C008829");
        });

        modelBuilder.Entity<CheckInCheckOut>(entity =>
        {
            entity.HasKey(e => e.CheckInId).HasName("SYS_C008864");

            entity.ToTable("CHECK_IN_CHECK_OUT");

            entity.Property(e => e.CheckInId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CHECK_IN_ID");
            entity.Property(e => e.CheckInDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_IN_DATE");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");

            entity.HasOne(d => d.Emp).WithMany(p => p.CheckInCheckOuts)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("SYS_C008865");
        });

        modelBuilder.Entity<Checkin>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.RoomId, e.InTime }).HasName("SYS_C008838");

            entity.ToTable("CHECKIN");

            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.RoomId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");
            entity.Property(e => e.InTime)
                .HasPrecision(6)
                .HasColumnName("IN_TIME");
            entity.Property(e => e.OutTime)
                .HasPrecision(6)
                .HasColumnName("OUT_TIME");

            entity.HasOne(d => d.Customer).WithMany(p => p.Checkins)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008839");

            entity.HasOne(d => d.Room).WithMany(p => p.Checkins)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008840");
        });

        modelBuilder.Entity<Checkrepair>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.AssetId,e.CheckRepairTime }).HasName("SYS_C008882");

            entity.ToTable("CHECKREPAIR");

            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.AssetId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ASSET_ID");
            entity.Property(e => e.CheckRepairContent)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("CHECK_REPAIR_CONTENT");
            entity.Property(e => e.CheckRepairTime)
                .HasPrecision(6)
                .HasColumnName("CHECK_REPAIR_TIME");

            entity.HasOne(d => d.Asset).WithMany(p => p.Checkrepairs)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008884");

            entity.HasOne(d => d.Emp).WithMany(p => p.Checkrepairs)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008883");
        });

        modelBuilder.Entity<Cleaning>(entity =>
        {
            entity.HasKey(e => new { e.RoomId, e.EmpId, e.CleaningTime }).HasName("SYS_C008832");

            entity.ToTable("CLEANING");

            entity.Property(e => e.RoomId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.CleaningTime)
                .HasPrecision(6)
                .HasColumnName("CLEANING_TIME");

            entity.HasOne(d => d.Emp).WithMany(p => p.Cleanings)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008834");

            entity.HasOne(d => d.Room).WithMany(p => p.Cleanings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008833");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("SYS_C008819");

            entity.ToTable("COMPLAINT");

            entity.Property(e => e.ComplaintId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COMPLAINT_ID");
            entity.Property(e => e.ComplaintType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COMPLAINT_TYPE");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Feedback)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FEEDBACK");
            entity.Property(e => e.Result)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RESULT");
            entity.Property(e => e.SubmitTime)
                .HasPrecision(6)
                .HasColumnName("SUBMIT_TIME");
            entity.Property(e => e.Way)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("WAY");

            entity.HasOne(d => d.Customer).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("SYS_C008820");
        });

        modelBuilder.Entity<Consume>(entity =>
        {
            entity.HasKey(e => new { e.DepartmentId, e.GoodsId }).HasName("SYS_C008868");

            entity.ToTable("CONSUME");

            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.GoodsId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("GOODS_ID");
            entity.Property(e => e.ConsumeQuantity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONSUME_QUANTITY");

            entity.HasOne(d => d.Department).WithMany(p => p.Consumes)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008869");

            entity.HasOne(d => d.Goods).WithMany(p => p.Consumes)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008870");
        });

        modelBuilder.Entity<ConsumptionRecord>(entity =>
        {
            entity.HasKey(e => e.ConsumeId).HasName("SYS_C008848");

            entity.ToTable("CONSUMPTION_RECORDS");

            entity.Property(e => e.ConsumeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONSUME_ID");
            entity.Property(e => e.BillId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BILL_ID");
            entity.Property(e => e.ConsumeAmount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("CONSUME_AMOUNT");
            entity.Property(e => e.ConsumeTime)
                .HasPrecision(6)
                .HasColumnName("CONSUME_TIME");
            entity.Property(e => e.ConsumeType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CONSUME_TYPE");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.RoomId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");

            entity.HasOne(d => d.Bill).WithMany(p => p.ConsumptionRecords)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("SYS_C008850");

            entity.HasOne(d => d.Employee).WithMany(p => p.ConsumptionRecords)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("SYS_C008851");

            entity.HasOne(d => d.Room).WithMany(p => p.ConsumptionRecords)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("SYS_C008849");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("SYS_C008803");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.CreditGrade)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CREDIT_GRADE");
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.MemberGrade)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MEMBER_GRADE");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("SYS_C008792");

            entity.ToTable("DEPARTMENT");

            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DEPARTMENT_NAME");
            entity.Property(e => e.NumberOfPeople)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NUMBER_OF_PEOPLE");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("SYS_C008802");

            entity.ToTable("DISH");

            entity.Property(e => e.DishId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DISH_ID");
            entity.Property(e => e.DishName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DISH_NAME");
            entity.Property(e => e.DishPrice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("DISH_PRICE");
            entity.Property(e => e.DishTaste)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("DISH_TASTE");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("SYS_C008797");

            entity.ToTable("EMPLOYEE");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Account)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ACCOUNT");
            entity.Property(e => e.Age)
                .HasPrecision(3)
                .HasColumnName("AGE");
            entity.Property(e => e.BankName)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("BANK_NAME");
            entity.Property(e => e.BasePay)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BASE_PAY");
            entity.Property(e => e.CreditCardNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CREDIT_CARD_NUMBER");
            entity.Property(e => e.EntryTime)
                .HasColumnType("DATE")
                .HasColumnName("ENTRY_TIME");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.PostId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POST_ID");
            entity.Property(e => e.Sex)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("SEX");

            entity.HasOne(d => d.Post).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("SYS_C008798");
        });

        modelBuilder.Entity<EnterExit>(entity =>
        {
            entity.HasKey(e => e.InfoId).HasName("SYS_C008855");

            entity.ToTable("ENTER_EXIT");

            entity.Property(e => e.InfoId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INFO_ID");
            entity.Property(e => e.CarNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CAR_NUMBER");
            entity.Property(e => e.EnterTime)
                .HasPrecision(6)
                .HasColumnName("ENTER_TIME");
            entity.Property(e => e.ExitTime)
                .HasPrecision(6)
                .HasColumnName("EXIT_TIME");

            entity.HasOne(d => d.CarNumberNavigation).WithMany(p => p.EnterExits)
                .HasForeignKey(d => d.CarNumber)
                .HasConstraintName("SYS_C008856");
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.GoodsId).HasName("SYS_C008800");

            entity.ToTable("GOODS");

            entity.Property(e => e.GoodsId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("GOODS_ID");
            entity.Property(e => e.Category)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.GoodsName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GOODS_NAME");
            entity.Property(e => e.Inventory)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("INVENTORY");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("SYS_C008871");

            entity.ToTable("LOCATION");

            entity.Property(e => e.LocationId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LOCATION_ID");
            entity.Property(e => e.Floor)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FLOOR");
            entity.Property(e => e.Note)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.RoomId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");

            entity.HasOne(d => d.Room).WithMany(p => p.Locations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("SYS_C008872");
        });

        modelBuilder.Entity<Maintain>(entity =>
        {
            entity.HasKey(e => new { e.RoomId, e.EmpId, e.MaintainTime }).HasName("SYS_C008845");

            entity.ToTable("MAINTAIN");

            entity.Property(e => e.RoomId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.MaintainTime)
                .HasPrecision(6)
                .HasColumnName("MAINTAIN_TIME");
            entity.Property(e => e.Object)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("OBJECT");
            entity.Property(e => e.Result)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("RESULT");

            entity.HasOne(d => d.Emp).WithMany(p => p.Maintains)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008847");

            entity.HasOne(d => d.Room).WithMany(p => p.Maintains)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008846");
        });

        modelBuilder.Entity<Myorder>(entity =>
        {
            entity.HasKey(e => new { e.TableId, e.DishId, e.OrderTime }).HasName("SYS_C008810");

            entity.ToTable("MYORDER");

            entity.Property(e => e.TableId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TABLE_ID");
            entity.Property(e => e.DishId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DISH_ID");
            entity.Property(e => e.OrderTime)
                .HasPrecision(6)
                .HasColumnName("ORDER_TIME");
            entity.Property(e => e.ConsumptionRecordId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONSUMPTION_RECORD_ID");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ORDER_STATUS");

            entity.HasOne(d => d.ConsumptionRecord).WithMany(p => p.Myorders)
                .HasForeignKey(d => d.ConsumptionRecordId)
                .HasConstraintName("SYS_C008813");

            entity.HasOne(d => d.Dish).WithMany(p => p.Myorders)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008812");

            entity.HasOne(d => d.Table).WithMany(p => p.Myorders)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008811");
        });

        modelBuilder.Entity<Mytable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("SYS_C008801");

            entity.ToTable("MYTABLE");

            entity.Property(e => e.TableId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TABLE_ID");
            entity.Property(e => e.Available)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AVAILABLE");
            entity.Property(e => e.Bookable)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKABLE");
            entity.Property(e => e.CapacityNum)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CAPACITY_NUM");
            entity.Property(e => e.Note)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.TableLocation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TABLE_LOCATION");
            entity.Property(e => e.TableStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TABLE_STATUS");
            entity.Property(e => e.TableType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TABLE_TYPE");
        });

        modelBuilder.Entity<ParkPlace>(entity =>
        {
            entity.HasKey(e => e.ParkingSpaceId).HasName("SYS_C008826");

            entity.ToTable("PARK_PLACE");

            entity.Property(e => e.ParkingSpaceId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PARKING_SPACE_ID");
            entity.Property(e => e.Area)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("AREA");
            entity.Property(e => e.Status)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.Type)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("TYPE");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.ParkPlaces)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("SYS_C008827");
        });

        modelBuilder.Entity<Parking>(entity =>
        {
            entity.HasKey(e => new { e.ParkingSpaceId, e.CarNumber, e.StartTime }).HasName("SYS_C008852");

            entity.ToTable("PARKING");

            entity.Property(e => e.ParkingSpaceId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PARKING_SPACE_ID");
            entity.Property(e => e.CarNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CAR_NUMBER");
            entity.Property(e => e.StartTime)
                .HasPrecision(6)
                .HasColumnName("START_TIME");
            entity.Property(e => e.EndTime)
                .HasPrecision(6)
                .HasColumnName("END_TIME");

            entity.HasOne(d => d.CarNumberNavigation).WithMany(p => p.Parkings)
                .HasForeignKey(d => d.CarNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008854");

            entity.HasOne(d => d.ParkingSpace).WithMany(p => p.Parkings)
                .HasForeignKey(d => d.ParkingSpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008853");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.BillId }).HasName("SYS_C008823");

            entity.ToTable("PAY");

            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.BillId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BILL_ID");
            entity.Property(e => e.PayCount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PAY_COUNT");
            entity.Property(e => e.PayTime)
                .HasPrecision(6)
                .HasColumnName("PAY_TIME");
            entity.Property(e => e.PayWay)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("PAY_WAY");
            entity.Property(e => e.Status)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("STATUS");

            entity.HasOne(d => d.Bill).WithMany(p => p.Pays)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008825");

            entity.HasOne(d => d.Customer).WithMany(p => p.Pays)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008824");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("SYS_C008795");

            entity.ToTable("POST");

            entity.Property(e => e.PostId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POST_ID");
            entity.Property(e => e.AccumulationFunds)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ACCUMULATION_FUNDS");
            entity.Property(e => e.Authority)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("AUTHORITY");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.Insurance)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("INSURANCE");
            entity.Property(e => e.PaymentBase)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_BASE");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_TYPE");
            entity.Property(e => e.PositionLevel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("POSITION_LEVEL");
            entity.Property(e => e.PositionSalary)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("POSITION_SALARY");
            entity.Property(e => e.PostName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("POST_NAME");

            entity.HasOne(d => d.Department).WithMany(p => p.Posts)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("SYS_C008796");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseId }).HasName("SYS_C008859");

            entity.ToTable("PURCHASE");

            entity.Property(e => e.PurchaseId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PURCHASE_ID");
            entity.Property(e => e.GoodsId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("GOODS_ID");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.SupplierId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("DATE")
                .HasColumnName("PURCHASE_DATE");
            entity.Property(e => e.PurchaseQuantity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PURCHASE_QUANTITY");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("UNIT_PRICE");

            entity.HasOne(d => d.Employee).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008861");

            entity.HasOne(d => d.Goods).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.GoodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008860");

            entity.HasOne(d => d.Suppliers).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008900");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("SYS_C008821");

            entity.ToTable("ROOM");

            entity.Property(e => e.RoomId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");
            entity.Property(e => e.RoomNum)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_NUM");
            entity.Property(e => e.RoomPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROOM_PHONE");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.TypeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TYPE_ID");

            entity.HasOne(d => d.Type).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("SYS_C008822");
        });

        modelBuilder.Entity<Roomorder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("SYS_C008841");

            entity.ToTable("ROOMORDER");

            entity.Property(e => e.OrderId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ORDER_ID");
            entity.Property(e => e.CreateTime)
                .HasPrecision(6)
                .HasColumnName("CREATE_TIME");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.ExpectInTime)
                .HasColumnType("DATE")
                .HasColumnName("EXPECT_IN_TIME");
            entity.Property(e => e.ExpectOutTime)
                .HasColumnType("DATE")
                .HasColumnName("EXPECT_OUT_TIME");
            entity.Property(e => e.Note)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.Num)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NUM");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ORDER_STATUS");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.TypeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TYPE_ID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Roomorders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("SYS_C008842");

            entity.HasOne(d => d.Emp).WithMany(p => p.Roomorders)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("SYS_C008844");

            entity.HasOne(d => d.Type).WithMany(p => p.Roomorders)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("SYS_C008843");
        });

        modelBuilder.Entity<Roomtype>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("SYS_C008806");

            entity.ToTable("ROOMTYPE");

            entity.Property(e => e.TypeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TYPE_ID");
            entity.Property(e => e.Note)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("NOTE");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Remaining)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("REMAINING");
            entity.Property(e => e.TypeName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TYPE_NAME");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("SYS_C008830");

            entity.ToTable("SALARY");

            entity.Property(e => e.SalaryId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ASSETS_ID");
            entity.Property(e => e.Bonus)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("BONUS");
            entity.Property(e => e.Commission)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("COMMISSION");
            entity.Property(e => e.EarlyDepartureDeduction)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("EARLY_DEPARTURE_DEDUCTION");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.GrossSalary)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("GROSS_SALARY");
            entity.Property(e => e.HolidayAllowance)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("HOLIDAY_ALLOWANCE");
            entity.Property(e => e.IncomeTax)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("INCOME_TAX");
            entity.Property(e => e.LateDeduction)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("LATE_DEDUCTION");
            entity.Property(e => e.Month)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MONTH");
            entity.Property(e => e.NetSalary)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("NET_SALARY");
            entity.Property(e => e.OtherAllowance)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("OTHER_ALLOWANCE");
            entity.Property(e => e.OvertimePay)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("OVERTIME_PAY");
            entity.Property(e => e.RewardAmount)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("REWARD_AMOUNT");
            entity.Property(e => e.RewardType)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("REWARD_TYPE");
            entity.Property(e => e.SocialInsurance)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("SOCIAL_INSURANCE");
            entity.Property(e => e.Year)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("YEAR");
            entity.Property(e => e.YearEndBonus)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("YEAR_END_BONUS");

            entity.HasOne(d => d.Emp).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("SYS_C008831");
        });

        modelBuilder.Entity<Scrap>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.AssetId }).HasName("SYS_C008879");

            entity.ToTable("SCRAP");

            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.AssetId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ASSET_ID");
            entity.Property(e => e.ScrapReason)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("SCRAP_REASON");
            entity.Property(e => e.ScrapTime)
                .HasPrecision(6)
                .HasColumnName("SCRAP_TIME");

            entity.HasOne(d => d.Asset).WithMany(p => p.Scraps)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008881");

            entity.HasOne(d => d.Emp).WithMany(p => p.Scraps)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008880");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.EmpId, e.ServiceTime }).HasName("SYS_C008835");

            entity.ToTable("SERVICE");

            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.EmpId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.ServiceTime)
                .HasPrecision(6)
                .HasColumnName("SERVICE_TIME");
            entity.Property(e => e.Result)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("RESULT");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SERVICE_TYPE");

            entity.HasOne(d => d.Customer).WithMany(p => p.Services)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008836");

            entity.HasOne(d => d.Emp).WithMany(p => p.Services)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_C008837");
        });

        modelBuilder.Entity<Staging>(entity =>
        {
            entity.HasKey(e => e.LuggageId).HasName("SYS_C008817");

            entity.ToTable("STAGING");

            entity.Property(e => e.LuggageId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LUGGAGE_ID");
            entity.Property(e => e.CustomerId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.FetchTime)
                .HasPrecision(6)
                .HasColumnName("FETCH_TIME");
            entity.Property(e => e.LuggageType)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("LUGGAGE_TYPE");
            entity.Property(e => e.StagingTime)
                .HasPrecision(6)
                .HasColumnName("STAGING_TIME");

            entity.HasOne(d => d.Customer).WithMany(p => p.Stagings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("SYS_C008818");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("SYS_C008799");

            entity.ToTable("SUPPLIER");

            entity.Property(e => e.SupplierId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.SupplierPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SUPPLIER_PHONE");
        });


        modelBuilder.Entity<UnitPrice>(entity =>
        {
            entity.HasKey(e => e.ParkingPlaceType).HasName("SYS_C008805");

            entity.ToTable("UNIT_PRICE");

            entity.Property(e => e.ParkingPlaceType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PARKING_PLACE_TYPE");
            entity.Property(e => e.MemberPrice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("MEMBER_PRICE");
            entity.Property(e => e.NotMemberPrice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("NOT_MEMBER_PRICE");
        });

		modelBuilder.Entity<AccountingSubject>().Property(p => p.AccountSubjectId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Application>().Property(p => p.ApplicationId).ValueGeneratedOnAdd();
		modelBuilder.Entity<AssetsInformation>().Property(p => p.AssetId).ValueGeneratedOnAdd();
		modelBuilder.Entity<AssetsSummarize>().Property(p => p.AssetsSummarizeId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Assignment>().Property(p => p.AssignmentId).ValueGeneratedOnAdd();
		modelBuilder.Entity<AttendanceInformation>().Property(p => p.AttendanceId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Bill>().Property(p => p.BillId).ValueGeneratedOnAdd();
		modelBuilder.Entity<CheckInCheckOut>().Property(p => p.CheckInId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Complaint>().Property(p => p.ComplaintId).ValueGeneratedOnAdd();
		modelBuilder.Entity<ConsumptionRecord>().Property(p => p.ConsumeId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Customer>().Property(p => p.CustomerId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Department>().Property(p => p.DepartmentId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Dish>().Property(p => p.DishId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Employee>().Property(p => p.EmployeeId).ValueGeneratedOnAdd();
		modelBuilder.Entity<EnterExit>().Property(p => p.InfoId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Good>().Property(p => p.GoodsId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Location>().Property(p => p.LocationId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Mytable>().Property(p => p.TableId).ValueGeneratedOnAdd();
		modelBuilder.Entity<ParkPlace>().Property(p => p.ParkingSpaceId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Post>().Property(p => p.PostId).ValueGeneratedOnAdd();
        modelBuilder.Entity<Purchase>().Property(p => p.PurchaseId).ValueGeneratedOnAdd();
        modelBuilder.Entity<Room>().Property(p => p.RoomId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Roomorder>().Property(p => p.OrderId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Roomtype>().Property(p => p.TypeId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Salary>().Property(p => p.SalaryId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Staging>().Property(p => p.LuggageId).ValueGeneratedOnAdd();
		modelBuilder.Entity<Supplier>().Property(p => p.SupplierId).ValueGeneratedOnAdd();

		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
