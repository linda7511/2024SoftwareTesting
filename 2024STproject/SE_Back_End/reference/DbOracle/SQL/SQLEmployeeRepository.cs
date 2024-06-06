using DbOracle.Entities;
using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLEmployeeRepository : IEmployeeRepository
	{
		private MyDbContext _context;

		public SQLEmployeeRepository(MyDbContext context)
		{
			_context = context;
		}

		public Employee Add(Employee employee)
		{
			try
			{
                _context.Employees.Add(employee);
                _context.SaveChanges();
                Employee temp = _context.Employees.FirstOrDefault(a=>a.Account==employee.Account);
                Post post= _context.Posts.FirstOrDefault(a => a.PostId == temp.PostId);
                Department department= _context.Departments.FirstOrDefault(a => a.DepartmentId == post.DepartmentId);
                department.NumberOfPeople+=1;
                _context.Departments.Update(department);
                _context.SaveChanges();
                return temp;
            }
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
			//return employee;
		}

		public bool Delete(decimal id)
		{
			try
			{
				var employee = _context.Employees.FirstOrDefault(a => a.EmployeeId == id);
				_context.Employees.Remove(employee);
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

		public Employee? Get(decimal id)
		{
			try
			{
				Employee employee = _context.Employees.FirstOrDefault(a => a.EmployeeId == id);
				if (employee == null)
				{
					return null;
				}
				return employee;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public Employee GetByAccount(string account)
        {
            try
            {
				Employee employee = _context.Employees.FirstOrDefault(a => a.Account == account);
				if (employee == null)
                {
					return null;
				}
				return employee;
			}
			catch (Exception e)
            {
				Console.WriteLine(e);
				//throw;
				return null;
			}
        }

        public IEnumerable<Employee>? GetByName(string employeeName)
        {
            try
            {
                var employees = _context.Employees.Where(a => a.Name == employeeName);
                if (employees == null)
                {
                    return null;
                }
                return employees;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CombinedDataEmployeeMessage>? GetAll()
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Employees
                    from t2 in _context.Posts
                    from t3 in _context.Departments
                    where t2.DepartmentId == t3.DepartmentId && t1.PostId == t2.PostId
                    select new CombinedDataEmployeeMessage
                    {
                        EmployeeId = t1.EmployeeId,
                        DepartmentId = t3.DepartmentId,
                        PostId = t2.PostId,
                        Name = t1.Name,
                        DepartmentName = t3.DepartmentName,
                        PostName = t2.PostName,

                    }
                    );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }
        public Employee? GetByEmpId(decimal emp_id)
        {
            try
            {
                Employee employee = _context.Employees.FirstOrDefault(a => a.EmployeeId == emp_id);
                if (employee == null)
                {
                    return null;
                }
                return employee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public Employee Update(Employee new_employee)
        {
            try
            {
                Employee temp_employee=null;
                if (new_employee.Account!=null)
                    temp_employee = _context.Employees.FirstOrDefault(a=>a.Account == new_employee.Account);
                var old_employee = _context.Employees.Find(new_employee.EmployeeId);
                if (temp_employee != null && temp_employee.EmployeeId != old_employee.EmployeeId)
                    return null;

                //更改岗位时修改部门人数
                if(new_employee.PostId!=null)
                {
                    Post old_post= _context.Posts.FirstOrDefault(a=>a.PostId == old_employee.PostId);
                    Post new_post= _context.Posts.FirstOrDefault(a => a.PostId == new_employee.PostId);
                    Department old_department= _context.Departments.FirstOrDefault(a=>a.DepartmentId == old_post.DepartmentId);
                    Department new_department = _context.Departments.FirstOrDefault(a => a.DepartmentId == new_post.DepartmentId);
                    old_department.NumberOfPeople -= 1;
                    new_department.NumberOfPeople += 1;
                    _context.Departments.Update(old_department);
                    _context.Departments.Update(new_department);
                }

                Type employeeType = typeof(Employee);
                PropertyInfo[] properties = employeeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(new_employee);
                    if (value != null)
                    {
                        property.SetValue(old_employee, value);
                    }
                }
                _context.Employees.Update(old_employee);
                _context.SaveChanges();
                return old_employee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
            //return true;
        }

        public LoginResult Login(string account, string password)
        {
            var employee = _context.Employees.FirstOrDefault(a => a.Account == account);

			LoginResult loginResult = new LoginResult();
            if (employee == null)
            {
                loginResult.StatusCode = -1;
                loginResult.Message = "账号输入错误，无对应员工信息";
            }
            else if (employee.Password == password)
            {
                loginResult.StatusCode = 0;
                loginResult.Message = "成功登录";
                loginResult.employeeInfo = employee;
			}
            else if (employee.Password != password)
            {
                loginResult.StatusCode = 1;
                loginResult.Message = "密码错误";
            }
            else
            {
                loginResult.StatusCode = 2;
                loginResult.Message = "未知错误";
            }
            return loginResult;
        }

    }
}

public class CombinedDataEmployeeMessage
{
    public decimal EmployeeId { get; set; }

    public decimal PostId { get; set; }

    public decimal? DepartmentId { get; set; }

    public string Name { get; set; }

    public string? DepartmentName { get; set; }

    public string? PostName { get; set; }
}

public class LoginResult
{
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public Employee? employeeInfo { get; set; }

    public string token { get; set; }
}

