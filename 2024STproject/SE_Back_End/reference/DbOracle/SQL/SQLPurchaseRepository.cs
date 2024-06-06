using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLPurchaseRepository : IPurchaseRepository
	{
		private MyDbContext _context;

		public SQLPurchaseRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Purchase purchase)
		{
			try
			{
				_context.Purchases.Add(purchase);
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

		public bool Delete(decimal purchase_id)
		{
			try
			{
				var purchase = _context.Purchases.FirstOrDefault(a => a.PurchaseId== purchase_id);
				_context.Purchases.Remove(purchase);
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



		public Purchase? Get(decimal purchase_id)
        {
			try
			{
				Purchase purchase = _context.Purchases.FirstOrDefault(a => a.PurchaseId== purchase_id);
				return purchase;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
			
		}
		public IEnumerable<Purchase>? GetByGoodEmpSupplier(decimal goods_id, decimal employee_id, decimal supplier_id)
		{
			try
			{
                var purchases = _context.Purchases.Where(a => a.GoodsId == goods_id && a.EmployeeId == employee_id&&a.SupplierId== supplier_id);

				if (purchases == null)
				{
					return null;
				}
				return purchases;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Purchase>? GetByGoodsId(decimal goods_id)
        {
			try
			{
				var purchases = _context.Purchases.Where(a => a.GoodsId== goods_id);
				return purchases;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Purchase>? GetByEmployeeId(decimal employee_id)
        {
			try
			{
				var purchases = _context.Purchases.Where(a => a.EmployeeId == employee_id);
				return purchases;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Purchase>? GetBySupplierId(decimal supplier_id)
        {
			try
			{
				var purchases = _context.Purchases.Where(a => a.SupplierId == supplier_id);
				return purchases;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}
		public IEnumerable<Purchase> GetPurchases()
		{
			try
			{
				return _context.Purchases;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
			
		}

		public bool Update(Purchase purchase)
		{
			try
			{
				var temp = _context.Purchases.Find(purchase.PurchaseId);
				if (temp == null)
				{
					Console.WriteLine("无对应的采购信息 更新失败");
					return false;
				}
				Type purchaseType = typeof(Purchase);
				PropertyInfo[] properties = purchaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

				foreach (PropertyInfo property in properties)
				{
					object value = property.GetValue(purchase);
					if (value != null)
					{
						property.SetValue(temp, value);
					}
				}
				_context.Purchases.Update(temp);
				_context.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return false;
			}
		}
	}
}
