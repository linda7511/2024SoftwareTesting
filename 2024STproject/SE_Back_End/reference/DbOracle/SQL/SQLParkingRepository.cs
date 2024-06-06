using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLParkingRepository : IParkingRepository
    {
        private MyDbContext _context;

        public SQLParkingRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Parking parking)
        {
            try
            {
                _context.Parkings.Add(parking);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }

        public bool Delete(decimal parking_space_id, string car_number, DateTime start_time)
        {
            try
            {
                var parking = _context.Parkings.FirstOrDefault(a => a.ParkingSpaceId == parking_space_id && a.CarNumber == car_number &&
                 a.StartTime == start_time );

                _context.Parkings.Remove(parking);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }

        public IEnumerable<CombinedDataOccupancy> GetByParkingAndPlace()
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    where t1.ParkingSpaceId == t2.ParkingSpaceId
                    select new CombinedDataOccupancy
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataParking> GetAll()
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    from t3 in _context.UnitPrices
                    from t4 in _context.Cars
                    from t5 in _context.Customers
                    where t1.ParkingSpaceId == t2.ParkingSpaceId &&
                          t2.Type == t3.ParkingPlaceType &&
                          t1.CarNumber == t4.CarNumber &&
                          t4.CustomerId == t5.CustomerId
                    select new CombinedDataParking
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                        EndTime = t1.EndTime,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        MemberPrice = t3.MemberPrice,
                        NotMemberPrice = t3.NotMemberPrice,
                        IsMember = t5.MemberGrade == "0" || t5.MemberGrade == null ? false : true,
                        TotalFee = CalculateTotalFee(t1.StartTime, t1.EndTime, t5.MemberGrade == "0" ? false : true, t3.MemberPrice, t3.NotMemberPrice)
                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataParking> GetByCarNumber(string car_number)
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    from t3 in _context.UnitPrices
                    from t4 in _context.Cars
                    from t5 in _context.Customers
                    where t1.ParkingSpaceId == t2.ParkingSpaceId &&
                          t2.Type == t3.ParkingPlaceType &&
                          t1.CarNumber == t4.CarNumber &&
                          t4.CustomerId == t5.CustomerId &&
                          t1.CarNumber == car_number
                    select new CombinedDataParking
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                        EndTime = t1.EndTime,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        MemberPrice = t3.MemberPrice,
                        NotMemberPrice = t3.NotMemberPrice,
                        IsMember = t5.MemberGrade == "0" || t5.MemberGrade == null ? false : true,
                        TotalFee = CalculateTotalFee(t1.StartTime, t1.EndTime, t5.MemberGrade == "0" ? false : true, t3.MemberPrice, t3.NotMemberPrice)
                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataParking> GetByTime(DateTime earliest, DateTime latest)
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    from t3 in _context.UnitPrices
                    from t4 in _context.Cars
                    from t5 in _context.Customers
                    where t1.ParkingSpaceId == t2.ParkingSpaceId &&
                          t2.Type == t3.ParkingPlaceType &&
                          t1.CarNumber == t4.CarNumber &&
                          t4.CustomerId == t5.CustomerId &&
                          earliest <= t1.StartTime && t1.EndTime <= latest
                    select new CombinedDataParking
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                        EndTime = t1.EndTime,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        MemberPrice = t3.MemberPrice,
                        NotMemberPrice = t3.NotMemberPrice,
                        IsMember = t5.MemberGrade == "0" || t5.MemberGrade == null ? false : true,
                        TotalFee = CalculateTotalFee(t1.StartTime, t1.EndTime, t5.MemberGrade == "0" ? false : true, t3.MemberPrice, t3.NotMemberPrice)
                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataParking> GetByParkingPlaceType(string type)
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    from t3 in _context.UnitPrices
                    from t4 in _context.Cars
                    from t5 in _context.Customers
                    where t1.ParkingSpaceId == t2.ParkingSpaceId &&
                          t2.Type == t3.ParkingPlaceType &&
                          t1.CarNumber == t4.CarNumber &&
                          t4.CustomerId == t5.CustomerId &&
                          t2.Type == type
                    select new CombinedDataParking
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                        EndTime = t1.EndTime,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        MemberPrice = t3.MemberPrice,
                        NotMemberPrice = t3.NotMemberPrice,
                        IsMember = t5.MemberGrade == "0" || t5.MemberGrade == null ? false : true,
                        TotalFee = CalculateTotalFee(t1.StartTime, t1.EndTime, t5.MemberGrade == "0" ? false : true, t3.MemberPrice, t3.NotMemberPrice)
                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataParking> GetByArea(string area)
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    from t3 in _context.UnitPrices
                    from t4 in _context.Cars
                    from t5 in _context.Customers
                    where t1.ParkingSpaceId == t2.ParkingSpaceId &&
                          t2.Type == t3.ParkingPlaceType &&
                          t1.CarNumber == t4.CarNumber &&
                          t4.CustomerId == t5.CustomerId &&
                          t2.Area == area
                    select new CombinedDataParking
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                        EndTime = t1.EndTime,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        MemberPrice = t3.MemberPrice,
                        NotMemberPrice = t3.NotMemberPrice,
                        IsMember = t5.MemberGrade == "0" || t5.MemberGrade == null ? false : true,
                        TotalFee = CalculateTotalFee(t1.StartTime, t1.EndTime, t5.MemberGrade == "0" ? false : true, t3.MemberPrice, t3.NotMemberPrice)
                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataParking> GetByStatus(string status)
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    from t3 in _context.UnitPrices
                    from t4 in _context.Cars
                    from t5 in _context.Customers
                    where t1.ParkingSpaceId == t2.ParkingSpaceId &&
                          t2.Type == t3.ParkingPlaceType &&
                          t1.CarNumber == t4.CarNumber &&
                          t4.CustomerId == t5.CustomerId &&
                          t2.Status == status
                    select new CombinedDataParking
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                        EndTime = t1.EndTime,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        MemberPrice = t3.MemberPrice,
                        NotMemberPrice = t3.NotMemberPrice,
                        IsMember = t5.MemberGrade == "0" || t5.MemberGrade == null ? false : true,
                        TotalFee = CalculateTotalFee(t1.StartTime, t1.EndTime, t5.MemberGrade == "0" ? false : true, t3.MemberPrice, t3.NotMemberPrice)
                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataParking> GetByPrice(decimal min_price, decimal max_price)
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Parkings
                    from t2 in _context.ParkPlaces
                    from t3 in _context.UnitPrices
                    from t4 in _context.Cars
                    from t5 in _context.Customers
                    where t1.ParkingSpaceId == t2.ParkingSpaceId &&
                          t2.Type == t3.ParkingPlaceType &&
                          t1.CarNumber == t4.CarNumber &&
                          t4.CustomerId == t5.CustomerId
                    select new CombinedDataParking
                    {
                        ParkingSpaceId = t1.ParkingSpaceId,
                        CarNumber = t1.CarNumber,
                        StartTime = t1.StartTime,
                        EndTime = t1.EndTime,
                        Type = t2.Type,
                        Area = t2.Area,
                        Status = t2.Status,
                        MemberPrice = t3.MemberPrice,
                        NotMemberPrice = t3.NotMemberPrice,
                        IsMember = t5.MemberGrade == "0" || t5.MemberGrade == null ? false : true,
                        TotalFee = CalculateTotalFee(t1.StartTime, t1.EndTime, t5.MemberGrade == "0" ? false : true, t3.MemberPrice, t3.NotMemberPrice)
                    }
                    ) ;

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public Parking? Get(decimal parking_space_id, string car_number, DateTime start_time)
        {
            try
            {
                Parking parking = _context.Parkings.FirstOrDefault(a => a.ParkingSpaceId == parking_space_id && a.CarNumber == car_number
                && a.StartTime == start_time );
                if (parking == null)
                {
                    return null;
                }
                return parking;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Parking new_parking)
        {
            try
            {
                var old_parking = _context.Parkings.FirstOrDefault(a => a.CarNumber == new_parking.CarNumber && a.EndTime == null);
                if (new_parking.EndTime != null)
                    old_parking.EndTime = new_parking.EndTime;
                _context.Parkings.Update(old_parking);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }

        // 自定义方法来计算总费用(一定要用静态函数)
        static private decimal CalculateTotalFee(DateTime startTime, DateTime? endTime,bool IsMember, decimal? memberPrice, decimal? notMemberPrice)
        {
            if (endTime == null)    
                endTime =  DateTime.Now;
            TimeSpan duration =(TimeSpan) (endTime - startTime);
            double days=duration.TotalDays;
            int totalDaysRounded = (int)Math.Ceiling(days);
            // 根据是否是会员来选择不同的价格
            memberPrice= memberPrice== null ? 0 : memberPrice;
            notMemberPrice= notMemberPrice== null ? 0 : notMemberPrice;
            decimal pricePerDay = (decimal)(IsMember ? memberPrice : notMemberPrice);
            // 计算总费用
            decimal totalFee = pricePerDay * (decimal)totalDaysRounded;
            return totalFee;
        }
    }
}
public class CombinedDataOccupancy
{
    public decimal ParkingSpaceId { get; set; }

    public string CarNumber { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public string? Type { get; set; }

    public string? Area { get; set; }

    public string? Status { get; set; }
}

public class CombinedDataParking
{
    public decimal ParkingSpaceId { get; set; }

    public string CarNumber { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Type { get; set; }

    public string? Area { get; set; }

    public string? Status { get; set; }

    public decimal? MemberPrice { get; set; }

    public decimal? NotMemberPrice { get; set; }

    public bool? IsMember { get; set; }

    public decimal? TotalFee { get; set; }
}



