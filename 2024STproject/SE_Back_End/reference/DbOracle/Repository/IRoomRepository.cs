using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IRoomRepository
	{
		public bool Add(Room room);

		public bool Delete(decimal room_id);

		public bool Update(Room room);

		public Room? Get(decimal room_id);

		int GetEmptyRoomCount(decimal typeId);

		public IEnumerable<Customer_Room>? GetMatchRooms(Options customerInfo);

		public IEnumerable<Room>? GetEmptyRoomsByType(decimal typeId);

		public IEnumerable<Room>? GetEmptyRoomsAll();

		public decimal? GetRoomIdByRoomNum(decimal roomNum);
	}
}
