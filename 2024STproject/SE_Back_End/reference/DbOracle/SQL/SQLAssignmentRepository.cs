using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLAssignmentRepository : IAssignmentRepository
	{
		private MyDbContext _context;

		public SQLAssignmentRepository(MyDbContext context)
		{
			_context = context;
		}

		public Assignment Add(Assignment assignment)
		{
			try
			{
				_context.Assignments.Add(assignment);
				_context.SaveChanges();
				return assignment;
			} catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
			//return true;
		}

		public bool Delete(decimal id)
		{
			try
			{
				var assignment = _context.Assignments.FirstOrDefault(a => a.AssignmentId == id);
				_context.Assignments.Remove(assignment);
				_context.SaveChanges();
			} catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return false;
			}
			return true;
		}

        public Assignment? Get(decimal id)
        {
            try
            {
                Assignment assignment = _context.Assignments.FirstOrDefault(a => a.AssignmentId == id);
                if (assignment == null)
                {
                    return null;
                }
                return assignment;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

		public IEnumerable<Assignment>? GetByDepartmentId(decimal departmentId)
        {
			try
			{
				var assignments = _context.Assignments.Where(a => a.DepartmentId == departmentId);
				return assignments;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Assignment>? GetByAssignmentName(string assignmentName)
        {
			try
			{
				var assignments = _context.Assignments.Where(a => a.AssignmentName.Contains(assignmentName));
				return assignments;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}
		public IEnumerable<Assignment>? GetAll()
		{
			try
			{
				var assignments = _context.Assignments;
				if (assignments == null)
				{
					return null;
				}
				return assignments;
			} catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}



		public bool Update(Assignment new_assignment)
		{
			try
			{
				//这里是利用反射写的
				var old_assignment = _context.Assignments.Find(new_assignment.AssignmentId);
				Type assignmentType = typeof(Assignment);
				PropertyInfo[] properties = assignmentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (PropertyInfo property in properties)
				{
					object value = property.GetValue(new_assignment);
					if (value != null)
					{
						property.SetValue(old_assignment, value);
					}
				}
				_context.Assignments.Update(old_assignment);
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
	}
}
