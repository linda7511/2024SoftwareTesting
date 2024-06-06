using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface ICheckInCheckOutRepository
    {
        public bool Add(CheckInCheckOut checkInCheckOut);

        public bool Delete(decimal id);

        public bool Update(CheckInCheckOut checkInCheckOut);

        public CheckInCheckOut? Get(decimal id);
    }
}
