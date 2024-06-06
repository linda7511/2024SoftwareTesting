using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLPayRepository : IPayRepository
	{
		private MyDbContext _context;

		public SQLPayRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Pay pay)
		{
			try
			{
				_context.Pays.Add(pay);
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

		public bool Delete(decimal customer_id, decimal bill_id)
		{
			try
			{
				var pay = _context.Pays.FirstOrDefault(a => a.CustomerId == customer_id&&a.BillId== bill_id);
				_context.Pays.Remove(pay);
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

		public Pay? Get(decimal customer_id, decimal bill_id)
		{
			try
			{
				Pay pay = _context.Pays.FirstOrDefault(a => a.CustomerId == customer_id && a.BillId == bill_id);
				if (pay == null)
				{
					return null;
				}
				return pay;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Pay>? GetAll()
        {
			try
			{
				return _context.Pays;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}
		public bool Update(Pay newPay)
		{
            try
            {
                var existedPay = _context.Pays.FirstOrDefault(a=>a.BillId == newPay.BillId && a.CustomerId == newPay.CustomerId);
                if (existedPay == null)
                {
                    Console.WriteLine("无对应的支付信息 更新失败");
                    return false;
                }
                Type payType = typeof(Pay);
                PropertyInfo[] properties = payType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newPay);
                    if (value != null)
                    {
                        property.SetValue(existedPay, value);
                    }
                }
                _context.Pays.Update(existedPay);
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
