using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLLocationRepository : ILocationRepository
	{
		private MyDbContext _context;

		public SQLLocationRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Location location)
		{
			try
			{
				_context.Locations.Add(location);
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
				var location = _context.Locations.FirstOrDefault(a => a.LocationId == id);
				_context.Locations.Remove(location);
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

		public Location? Get(decimal id)
		{
			try
			{
				Location location = _context.Locations.FirstOrDefault(a => a.LocationId == id);
				if (location == null)
				{
					return null;
				}
				return location;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public bool Update(Location newLocation)
		{
            try
            {
                var existedLocation = _context.Locations.Find(newLocation.LocationId);
                if (existedLocation == null)
                {
                    Console.WriteLine("无对应的位置信息 更新失败");
                    return false;
                }
                Type locationType = typeof(Location);
                PropertyInfo[] properties = locationType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newLocation);
                    if (value != null)
                    {
                        property.SetValue(existedLocation, value);
                    }
                }
                _context.Locations.Update(existedLocation);
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
