using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLUnitPriceRepository : IUnitPriceRepository
	{
		private MyDbContext _context;

		public SQLUnitPriceRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(UnitPrice unitPrice)
		{
			try
			{
				_context.UnitPrices.Add(unitPrice);
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

		public bool Delete(string parking_place_type)
		{
			try
			{
				var unitPrice = _context.UnitPrices.FirstOrDefault(a => a.ParkingPlaceType == parking_place_type);
				_context.UnitPrices.Remove(unitPrice);
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

		public UnitPrice? Get(string parking_place_type)
		{
			try
			{
				UnitPrice unitPrice = _context.UnitPrices.FirstOrDefault(a => a.ParkingPlaceType == parking_place_type);
				if (unitPrice == null)
				{
					return null;
				}
				return unitPrice;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

        public IEnumerable<CombinedDataParkingPrice>? GetAll()
        {
            try
            {
                var data =
                    (
                    from t1 in _context.UnitPrices
                    from t2 in _context.ParkPlaces
                    where t1.ParkingPlaceType == t2.Type
                    select new CombinedDataParkingPrice
                    {
                        ParkingSpaceId = t2.ParkingSpaceId,
                        MemberPrice = t1.MemberPrice,
                        NotMemberPrice = t1.NotMemberPrice,
                        Type = t2.Type,
                        Area = t2.Area,
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
        public bool Update(UnitPrice new_unitPrice)
        {
            try
            {
                var old_unitPrice = _context.UnitPrices.Find(new_unitPrice.ParkingPlaceType);
                Type unitPricesType = typeof(UnitPrice);
                PropertyInfo[] properties = unitPricesType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(new_unitPrice);
                    if (value != null)
                    {
                        property.SetValue(old_unitPrice, value);
                    }
                }
                _context.UnitPrices.Update(old_unitPrice);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }
    }
}

public class CombinedDataParkingPrice
{
    public decimal? ParkingSpaceId { get; set; }
    public decimal? MemberPrice { get; set; }

    public decimal? NotMemberPrice { get; set; }

    public string? Type { get; set; }

    public string? Area { get; set; }
}
