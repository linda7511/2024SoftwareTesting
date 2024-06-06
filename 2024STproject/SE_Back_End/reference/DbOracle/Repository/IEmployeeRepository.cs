using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IEmployeeRepository
	{
		public Employee Add(Employee employee);

		public bool Delete(decimal employee_id);

		public Employee Update(Employee employee);

		public Employee? Get(decimal employee_id);

		public Employee? GetByEmpId(decimal employeeID);


		public Employee? GetByAccount(string account);

		public IEnumerable<Employee>? GetByName(string employeeName);

		public IEnumerable<CombinedDataEmployeeMessage>? GetAll();

		public LoginResult Login(string account, string password);

    }
}
