using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLGoodRepository : IGoodRepository
	{
		private MyDbContext _context;

		public SQLGoodRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Good good)
		{
			try
			{
				_context.Goods.Add(good);
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
				var good = _context.Goods.FirstOrDefault(a => a.GoodsId == id);
				_context.Goods.Remove(good);
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

		public Good? Get(decimal id)
		{
			try
			{
				Good good = _context.Goods.FirstOrDefault(a => a.GoodsId == id);
				if (good == null)
				{
					return null;
				}
				return good;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

        public Good? GetByName(string goodsName)
		{
            try
            {
                Good good = _context.Goods.FirstOrDefault(a => a.GoodsName == goodsName);
                return good;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IEnumerable<Good>? GetAllInfo()
        {
            try
            {
				return _context.Goods;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Good newGood)
		{
            try
            {
                var existedGood = _context.Goods.Find(newGood.GoodsId);
                if (existedGood == null)
                {
                    Console.WriteLine("无对应的货品信息 更新失败");
                    return false;
                }
                Type goodType = typeof(Good);
                PropertyInfo[] properties = goodType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newGood);
                    if (value != null)
                    {
                        property.SetValue(existedGood, value);
                    }
                }
                _context.Goods.Update(existedGood);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public IEnumerable<Emp_Good>? GetByEmpId(decimal EmpId)
		{
            try
            {
                var result =
                     (
					 from purchase in _context.Purchases
					 from goods in _context.Goods
					 where purchase.EmployeeId == EmpId && goods.GoodsId==purchase.GoodsId
                     select new Emp_Good
                     {
                         EmployeeId = purchase.EmployeeId,
                         Category = goods.Category,
                         GoodsId = goods.GoodsId,
                         GoodsName = goods.GoodsName,
                         Inventory = goods.Inventory
                     }
                     );
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

    }
}

public class Emp_Good
{
    public decimal EmployeeId { get; set; }

    public decimal GoodsId { get; set; }

    public string? Category { get; set; }

    public string? GoodsName { get; set; }

    public decimal? Inventory { get; set; }
}
