using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLScrapRepository : IScrapRepository
	{
		private MyDbContext _context;

		public SQLScrapRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Scrap scrap)
		{
			try
			{
				var asset = _context.AssetsInformations.FirstOrDefault(a => a.AssetId == scrap.AssetId);
				if (asset == null)
				{
					Console.WriteLine("无对应资产信息 报废失败");
					return false;
				}
				asset.Valid = "报废";
				_context.AssetsInformations.Update(asset);
				_context.Scraps.Add(scrap);
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

		public bool Delete(decimal emp_id, decimal asset_id)
		{
			try
			{
				var scrap = _context.Scraps.FirstOrDefault(a => a.EmpId == emp_id&& a.AssetId== asset_id);
				_context.Scraps.Remove(scrap);
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

		public Scrap? Get(decimal emp_id, decimal asset_id)
		{
			try
			{
				Scrap scrap = _context.Scraps.FirstOrDefault(a => a.EmpId == emp_id && a.AssetId == asset_id);
				if (scrap == null)
				{
					return null;
				}
				return scrap;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public bool Update(Scrap newScrap)
		{
            try
            {
				var existedScrap = _context.Scraps.FirstOrDefault(a=>a.AssetId == newScrap.AssetId && a.EmpId == newScrap.EmpId);
                if (existedScrap == null)
                {
                    Console.WriteLine("无对应的报废信息 更新失败");
                    return false;
                }
                Type scrapType = typeof(Scrap);
                PropertyInfo[] properties = scrapType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newScrap);
                    if (value != null)
                    {
                        property.SetValue(existedScrap, value);
                    }
                }
                _context.Scraps.Update(existedScrap);
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
