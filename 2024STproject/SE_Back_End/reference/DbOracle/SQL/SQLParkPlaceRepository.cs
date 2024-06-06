using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLParkPlaceRepository : IParkPlaceRepository
	{
		private MyDbContext _context;

		public SQLParkPlaceRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(ParkPlace parkPlace)
		{
			try
			{
				_context.ParkPlaces.Add(parkPlace);
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

		public bool Delete(decimal id)
		{
			try
			{
				var parkPlace = _context.ParkPlaces.FirstOrDefault(a => a.ParkingSpaceId == id);
				_context.ParkPlaces.Remove(parkPlace);
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

		public ParkPlace? Get(decimal id)
		{
			try
			{
				ParkPlace parkPlace = _context.ParkPlaces.FirstOrDefault(a => a.ParkingSpaceId == id);
				if (parkPlace == null)
				{
					return null;
				}
				return parkPlace;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable< ParkPlace>? GetAll()
        {
			try
			{
				return _context.ParkPlaces;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public bool Update(ParkPlace newParkPlace)
		{
            try
            {
                var existedParkPlace = _context.ParkPlaces.Find(newParkPlace.ParkingSpaceId);
                if (existedParkPlace == null)
                {
                    Console.WriteLine("无对应的车位信息 更新失败");
                    return false;
                }
                Type parkPlaceType = typeof(ParkPlace);
                PropertyInfo[] properties = parkPlaceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newParkPlace);
                    if (value != null)
                    {
                        property.SetValue(existedParkPlace, value);
                    }
                }
                _context.ParkPlaces.Update(existedParkPlace);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
	}
}
