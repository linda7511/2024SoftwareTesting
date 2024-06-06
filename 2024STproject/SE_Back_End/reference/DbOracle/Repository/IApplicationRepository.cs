using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IApplicationRepository
    {
        public Application Add(Application application);

        public bool Delete(decimal id);

        public bool Update(Application application);

        public Application? Get(decimal id);

        public IEnumerable<Application>? GetAll();

        public IEnumerable<Application>? GetByEmpId(decimal employeeID);

        public IEnumerable<Application>? GetByType(string type);

        public IEnumerable<Application>? GetByApplicationTimeOfDay(DateTime applicationTime);
    }
}
