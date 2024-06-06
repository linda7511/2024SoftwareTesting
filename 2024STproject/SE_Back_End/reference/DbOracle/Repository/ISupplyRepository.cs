using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface ISupplyRepository
	{
		public bool Add(Supply supply);

		public bool Delete(decimal supplier_id,decimal goods_id);

		public bool Update(Supply supply);

		public Supply? Get(decimal supplier_id, decimal goods_id);

		public bool AddSupplySupplier(Supply_Supplier supply_supplier);

    }
}
