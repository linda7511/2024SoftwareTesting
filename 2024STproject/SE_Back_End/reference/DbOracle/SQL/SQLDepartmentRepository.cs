using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLDepartmentRepository : IDepartmentRepository
    {
        private MyDbContext _context;

        public SQLDepartmentRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Department department)
        {
            try
            {
                _context.Departments.Add(department);
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
                var department = _context.Departments.FirstOrDefault(a => a.DepartmentId == id);
                _context.Departments.Remove(department);
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

        public Department? Get(decimal id)
        {
            try
            {
                Department department = _context.Departments.FirstOrDefault(a => a.DepartmentId == id);
                if (department == null)
                {
                    return null;
                }
                return department;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable< Department>? GetAll()
        {
            try
            {
                return _context.Departments;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public Department? GetByName(string name)
        {
            try
            {
                Department department = _context.Departments.FirstOrDefault(a => a.DepartmentName == name);
                if (department == null)
                {
                    return null;
                }
                return department;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Department newDepartment)
        {
            try
            {
                var existedDepartment = _context.Departments.Find(newDepartment.DepartmentId);
                if (existedDepartment == null)
                {
                    Console.WriteLine("无对应的部门信息 更新失败");
                    return false;
                }
                Type departmentType = typeof(Department);
                PropertyInfo[] properties = departmentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newDepartment);
                    if (value != null)
                    {
                        property.SetValue(existedDepartment, value);
                    }
                }
                _context.Departments.Update(existedDepartment);
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
