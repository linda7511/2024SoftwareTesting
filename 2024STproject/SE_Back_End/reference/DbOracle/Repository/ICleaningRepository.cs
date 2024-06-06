using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface ICleaningRepository
    {
        public bool Add(Room_Cleaning room_Cleaning);

        public bool Delete(decimal RoomId, decimal EmpId, DateTime CleaningTime);

        public bool Update(Cleaning cleaning);

        public Cleaning? Get(decimal RoomId, decimal EmpId, DateTime CleaningTime);

        public IEnumerable<Cleaning>? GetByRoomId(decimal RoomId);

        public IEnumerable<Cleaning>? GetByEmpId(decimal EmpId);

    }
}
