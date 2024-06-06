using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IRoomorderRepository
	{
		public bool Add(Roomorder roomOrder);

		public bool Delete(decimal order_id);

        public bool Update(Roomorder roomOrder);

		public Roomorder? Get(decimal order_id);

		public IEnumerable<Roomorder>? GetAll();

		public IEnumerable<Roomorder>? GetByCustomerId(decimal CustomerId);
	}
}
