using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IMyorderRepository
	{
		public bool Add(Myorder myOrder);

		public bool Delete(decimal table_id,decimal dish_id,DateTime order_time);

		public bool Update(Myorder myOrder);

		public Myorder? Get(decimal table_id, decimal dish_id, DateTime order_time);

		public IEnumerable<OrderAndDish>? GetOrderAndDishInfo(decimal tableId);

    }
}
