using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLSupplierRepository : ISupplierRepository
	{
		private MyDbContext _context;

		public SQLSupplierRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Supplier supplier)
		{
			try
			{
				_context.Suppliers.Add(supplier);
				_context.SaveChanges();
			} catch (Exception e)
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
				var supplier = _context.Suppliers.FirstOrDefault(a => a.SupplierId == id);
				_context.Suppliers.Remove(supplier);
				_context.SaveChanges();
			} catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return false;
			}
			return true;
		}

		public Supplier? Get(decimal id)
		{
			try
			{
				Supplier supplier = _context.Suppliers.FirstOrDefault(a => a.SupplierId == id);
				if (supplier == null)
				{
					return null;
				}
				return supplier;
			} catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

        public IEnumerable<Supplier>? GetByGoodsId(decimal goodsId)
        {
            try
            {
				var supplierList =
					(
					from purchase in _context.Purchases
					from supplier in _context.Suppliers
					where purchase.GoodsId == goodsId && purchase.SupplierId == supplier.SupplierId
					select supplier
					);
                return supplierList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Supplier newSupplier)
		{
            try
            {
                var existedSupplier = _context.Suppliers.Find(newSupplier.SupplierId);
                if (existedSupplier == null)
                {
                    Console.WriteLine("无对应的供应商信息 更新失败");
                    return false;
                }
                Type supplierType = typeof(Supplier);
                PropertyInfo[] properties = supplierType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newSupplier);
                    if (value != null)
                    {
                        property.SetValue(existedSupplier, value);
                    }
                }
                _context.Suppliers.Update(existedSupplier);
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
