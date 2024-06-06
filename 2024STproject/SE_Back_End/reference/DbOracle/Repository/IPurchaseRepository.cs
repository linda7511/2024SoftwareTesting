using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IPurchaseRepository
	{
		public bool Add(Purchase purchase);

		public bool Delete(decimal purchase_id);

		public bool Update(Purchase purchase);

		public Purchase? Get(decimal purchase_id);
		public IEnumerable<Purchase>? GetByGoodEmpSupplier(decimal goods_id, decimal employee_id, decimal supplier_id);

		public IEnumerable<Purchase>? GetByGoodsId(decimal goods_id);

		public IEnumerable<Purchase>? GetByEmployeeId(decimal employee_id);

		public IEnumerable<Purchase>? GetBySupplierId(decimal supplier_id);

		IEnumerable<Purchase> GetPurchases();

    }
}
