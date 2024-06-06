using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface ICheckinRepository
    {
        public bool Add(Checkin checkin);

        public bool Delete(decimal CustomerId, decimal RoomId, DateTime InTime);

        public bool Update(Checkin checkin);

        public Checkin? Get(decimal CustomerId, decimal RoomId, DateTime InTime);

        public IEnumerable<Checkin>? GetByRoomNum(decimal roomNum);

        public IEnumerable<Checkin>? GetByCustomerIdAndInTime(decimal customerId, DateTime inTime);

        public IEnumerable<Checkin>? GetAll();
    }
}
