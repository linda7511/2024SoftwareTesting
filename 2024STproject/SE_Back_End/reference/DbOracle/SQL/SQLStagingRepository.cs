using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLStagingRepository : IStagingRepository
	{
		private MyDbContext _context;

		public SQLStagingRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Staging staging)
		{
			try
			{
				_context.Stagings.Add(staging);
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
				var staging = _context.Stagings.FirstOrDefault(a => a.LuggageId == id);
				_context.Stagings.Remove(staging);
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

		public Staging? Get(decimal id)
		{
			try
			{
				Staging staging = _context.Stagings.FirstOrDefault(a => a.LuggageId == id);
				if (staging == null)
				{
					return null;
				}
				return staging;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public bool Update(Staging newStaging)
		{
            try
            {
                var existedStaging = _context.Stagings.Find(newStaging.LuggageId);
                if (existedStaging == null)
                {
                    Console.WriteLine("无对应的行李暂存信息 更新失败");
                    return false;
                }
                Type stagingType = typeof(Staging);
                PropertyInfo[] properties = stagingType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newStaging);
                    if (value != null)
                    {
                        property.SetValue(existedStaging, value);
                    }
                }
                _context.Stagings.Update(existedStaging);
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
