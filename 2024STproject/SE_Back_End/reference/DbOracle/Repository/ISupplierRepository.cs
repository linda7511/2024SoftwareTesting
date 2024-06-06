using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface ISupplierRepository
	{
		public bool Add(Supplier supplier);
		public bool Delete(decimal id);
		public Supplier? Get(decimal id);
		public bool Update(Supplier supplier);
		public IEnumerable<Supplier>? GetByGoodsId(decimal goodsId);
    }
}
