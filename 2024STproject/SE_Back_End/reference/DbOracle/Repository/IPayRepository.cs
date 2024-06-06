using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IPayRepository
	{
		public bool Add(Pay pay);

		public bool Delete(decimal customer_id,decimal bill_id);

		public bool Update(Pay pay);

		public Pay? Get(decimal customer_id, decimal bill_id);
		public IEnumerable<Pay>? GetAll();
	}
}
