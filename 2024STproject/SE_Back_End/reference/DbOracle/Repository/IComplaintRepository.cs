using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IComplaintRepository
    {
        public bool Add(Complaint complaint);

        public bool Delete(decimal id);

        public bool Update(Complaint complaint);

        public Complaint? Get(decimal id);
    }
}
