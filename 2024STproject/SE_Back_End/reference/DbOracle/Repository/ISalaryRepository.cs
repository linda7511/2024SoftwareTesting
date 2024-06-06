using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface ISalaryRepository
	{
		public bool Add(Salary salary);

		public bool Delete(decimal assets_id);

		public bool Update(Salary salary);

		public Salary? Get(decimal assets_id);

		public IEnumerable<Salary>? GetByEmployeeID(decimal employeeID);

    }
}
