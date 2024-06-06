using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IDepartmentRepository
    {
        public bool Add(Department department);

        public bool Delete(decimal id);

        public bool Update(Department department);

        public Department? Get(decimal id);

        public IEnumerable<Department>? GetAll();

        public Department? GetByName(string name);
    }
}
