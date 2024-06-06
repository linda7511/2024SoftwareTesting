using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IAssignmentRepository
	{
		public Assignment Add(Assignment assignment);

		public bool Delete(decimal id);

		public bool Update(Assignment assignment);

		public Assignment? Get(decimal id);

		public IEnumerable<Assignment>? GetByDepartmentId(decimal departmentId);

		public IEnumerable<Assignment>? GetByAssignmentName(string assignmentName);

		public IEnumerable<Assignment>? GetAll();
	}
}
