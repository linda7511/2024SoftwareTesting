using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLSalaryRepository : ISalaryRepository
	{
		private MyDbContext _context;

		public SQLSalaryRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Salary salary)
		{
			try
			{
				_context.Salaries.Add(salary);
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

		public bool Delete(decimal id)
		{
			try
			{
				var salary = _context.Salaries.FirstOrDefault(a => a.SalaryId == id);
				_context.Salaries.Remove(salary);
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

		public Salary? Get(decimal id)
		{
			try
			{
				Salary salary = _context.Salaries.FirstOrDefault(a => a.SalaryId == id);
				if (salary == null)
				{
					return null;
				}
				return salary;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

        public IEnumerable<Salary>? GetByEmployeeID(decimal employeeID)
        {
            try
            {
                var salaries = _context.Salaries.Where(a => a.EmpId == employeeID);
                if (salaries == null)
                {
                    return null;
                }
                return salaries;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Salary newSalary)
		{
            try
            {
                var existedSalary = _context.Salaries.Find(newSalary.SalaryId);
                if (existedSalary == null)
                {
                    Console.WriteLine("无对应的工资信息 更新失败");
                    return false;
                }
                Type salaryType = typeof(Salary);
                PropertyInfo[] properties = salaryType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newSalary);
                    if (value != null)
                    {
                        property.SetValue(existedSalary, value);
                    }
                }
                _context.Salaries.Update(existedSalary);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
	}
}
