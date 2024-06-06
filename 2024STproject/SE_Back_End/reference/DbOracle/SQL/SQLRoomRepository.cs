using DbOracle.Models;
using DbOracle.Repository;
using System;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLRoomRepository : IRoomRepository
	{
		private MyDbContext _context;

		public SQLRoomRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Room room)
		{
			try
			{
				_context.Rooms.Add(room);
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
				var room = _context.Rooms.FirstOrDefault(a => a.RoomId == id);
				_context.Rooms.Remove(room);
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

		public Room? Get(decimal id)
		{
			try
			{
				Room room = _context.Rooms.FirstOrDefault(a => a.RoomId == id);
				if (room == null)
				{
					return null;
				}
				return room;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public bool Update(Room newRoom)
		{
            try
            {
                var existedRoom = _context.Rooms.Find(newRoom.RoomId);
                if (existedRoom == null)
                {
                    Console.WriteLine("无对应的客房信息 更新失败");
                    return false;
                }
                Type roomType = typeof(Room);
                PropertyInfo[] properties = roomType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newRoom);
                    if (value != null)
                    {
                        property.SetValue(existedRoom, value);
                    }
                }
                _context.Rooms.Update(existedRoom);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

		// 查找某个类型的空房数量
		public int GetEmptyRoomCount(decimal typeId)
		{
			try
			{
				var roomCount = _context.Rooms.Count(a => a.TypeId == typeId && a.Status == "空闲");
				return roomCount;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return 0;
			}
		}	

		public IEnumerable<Customer_Room>? GetMatchRooms(Options customerInfo)
		{
            try
			{ 
				var roomList =
					(
					from customer in _context.Customers
					where customer.Id != null &&customer.Id == customerInfo.Id   
					   || customer.Phone != null && customer.Phone == customerInfo.Phone 
                       || customer.Name != null  && customer.Name == customerInfo.Name
                    select new Customer_Room
					{
                        CustomerId = customer.CustomerId,
						rooms = (from order in _context.Roomorders
                                 from room in _context.Rooms
                                 where order.CustomerId == customer.CustomerId
                                     && room.TypeId == order.TypeId
                                 select room).ToList()
					}
					) ;
				if (roomList == null)
				{
                    Console.WriteLine("无匹配的房间信息 查询失败");
					return null;
                }
				return roomList;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

		public IEnumerable<Room>? GetEmptyRoomsByType(decimal typeId)
        {
			try
			{
				var rooms = _context.Rooms.Where(a => a.TypeId == typeId && a.Status == "空闲");
				return rooms;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Room>? GetEmptyRoomsAll()
        {
			try
			{
				var rooms = _context.Rooms.Where( a=>a.Status == "空闲");
				return rooms;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public decimal? GetRoomIdByRoomNum(decimal roomNum)
        {
			try
			{
				Room room = _context.Rooms.FirstOrDefault(a => a.RoomNum == roomNum);
				if (room != null)
					return room.RoomId;
				else
					return null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}
	}
}

public class Customer_Room
{
    public decimal CustomerId { get; set; }

	public IEnumerable<Room> rooms { get; set; }
}

public class Options
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }
}
