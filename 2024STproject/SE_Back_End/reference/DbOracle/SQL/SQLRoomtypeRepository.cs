using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLRoomtypeRepository : IRoomtypeRepository
	{
		private MyDbContext _context;

		public SQLRoomtypeRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Roomtype roomType)
		{
			try
			{
				_context.Roomtypes.Add(roomType);
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
				var roomType = _context.Roomtypes.FirstOrDefault(a => a.TypeId== id);
				_context.Roomtypes.Remove(roomType);
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

		public Roomtype? Get(decimal id)
		{
			try
			{
				Roomtype roomType = _context.Roomtypes.FirstOrDefault(a => a.TypeId == id);
				if (roomType == null)
				{
					return null;
				}
				return roomType;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

        public IEnumerable<CombinedDataRoomtype>? GetByType(string typeName)
        {
            try
            {
                var data =
                (
                from t1 in _context.Roomtypes
                from t2 in _context.Rooms
                where t1.TypeName == typeName && t1.TypeId == t2.TypeId
                select new CombinedDataRoomtype
                {
                    RoomId = t2.RoomId,
                    TypeId = t1.TypeId,
                    RoomNum = t2.RoomNum,
                    RoomPhone = t2.RoomPhone,
                    Status = t2.Status,
                    TypeName = t1.TypeName,
                    Price = t1.Price,
                    Note = t1.Note
                }
                );

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Roomtype newRoomtype)
		{
            try
            {
                var existedRoomtype = _context.Roomtypes.Find(newRoomtype.TypeId);
                if (existedRoomtype == null)
                {
                    Console.WriteLine("无对应的房型信息 更新失败");
                    return false;
                }
                Type roomtype = typeof(Roomtype);
                PropertyInfo[] properties = roomtype.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newRoomtype);
                    if (value != null)
                    {
                        property.SetValue(existedRoomtype, value);
                    }
                }
                _context.Roomtypes.Update(existedRoomtype);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

		public IEnumerable<Roomtype> GetBetween(decimal price_up, decimal price_down)
		{
			try
			{
				var roomTypes = _context.Roomtypes.Where(a => a.Price >= price_down && a.Price <= price_up);
				return roomTypes;
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

public class CombinedDataRoomtype
{
    public decimal RoomId { get; set; }

    public decimal? TypeId { get; set; }

    public decimal? RoomNum { get; set; }

    public string? RoomPhone { get; set; }

    public string? Status { get; set; }

    public string? TypeName { get; set; }

    public decimal? Price { get; set; }

    public string? Note { get; set; }
}