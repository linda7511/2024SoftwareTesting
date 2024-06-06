using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLSupplyRepository : ISupplyRepository
	{
		private MyDbContext _context;

		public SQLSupplyRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Supply supply)
		{
			try
			{
				_context.Supplies.Add(supply);
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

		public bool AddSupplySupplier(Supply_Supplier supply_supplier)
		{
			try
			{
				// 首先查找供应商表 如果找不到对应的供应商则添加
				var supplier = _context.Suppliers.FirstOrDefault(a => a.SupplierPhone == supply_supplier.SupplierPhone);
				if (supplier == null)
				{
					supplier = new Supplier();
					supplier.Address = supply_supplier.Address;
					supplier.SupplierPhone = supply_supplier.SupplierPhone;
					_context.Suppliers.Add(supplier);
                    _context.SaveChanges();
                }

				// 查找是否已经存在了id
				Supply supply = new Supply();
				supply.SupplierId = supplier.SupplierId;
				supply.GoodsId = supply_supplier.GoodsId;
				supply.Price = supply_supplier.Price;
				supply.SurplusQuantity = supply_supplier.SurplusQuantity;
                var savedSupply = _context.Supplies.Find(supply.SupplierId, supply.GoodsId);

                // 如果已经包含了相同的id，则只需要更新 否则为插入
                if (savedSupply != null)
					Update(supply);
				else
					_context.Supplies.Add(supply);
				_context.SaveChanges();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}
			return true;
		}

		public bool Delete(decimal supplier_id, decimal goods_id)
		{
			try
			{
				var supply = _context.Supplies.FirstOrDefault(a => a.SupplierId == supplier_id&&a.GoodsId== goods_id);
				_context.Supplies.Remove(supply);
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

		public Supply? Get(decimal supplier_id, decimal goods_id)
		{
			try
			{
				Supply supply = _context.Supplies.FirstOrDefault(a => a.SupplierId == supplier_id && a.GoodsId == goods_id);
				if (supply == null)
				{
					return null;
				}
				return supply;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public bool Update(Supply supply)
		{
            try
            {
                var newSupply = _context.Supplies.Find(supply.SupplierId, supply.GoodsId);
                if (newSupply == null)
                {
                    Console.WriteLine("无对应的供应表信息 更新失败");
                    return false;
                }
                Type supplyType = typeof(Supply);
                PropertyInfo[] properties = supplyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(supply);
                    if (value != null)
                    {
                        property.SetValue(newSupply, value);
                    }
                }
                _context.Supplies.Update(newSupply);
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

public class Supply_Supplier
{
    public decimal GoodsId { get; set; }

    public decimal? Price { get; set; }

    public decimal? SurplusQuantity { get; set; }

    public string? SupplierPhone { get; set; }

    public string? Address { get; set; }
}

