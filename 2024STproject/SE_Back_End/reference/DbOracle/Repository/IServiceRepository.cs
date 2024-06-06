using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IServiceRepository
	{
		public bool Add(Service service);

		public bool Delete(decimal customer_id,decimal emp_id,DateTime service_time);

		public bool Update(Service service);

		public Service? Get(decimal customer_id, decimal emp_id, DateTime service_time);
	}
}
