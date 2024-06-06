using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLMaintainRepository : IMaintainRepository
	{
		private MyDbContext _context;

		public SQLMaintainRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Room_maintain room_maintain)
		{
			try
			{
                var room = _context.Rooms.FirstOrDefault(r => r.RoomNum == room_maintain.RoomNum);
                if (room == null)
                {
                    Console.WriteLine("无对应房间信息，查询失败");
                    return false;
                }
                Maintain maintain = new Maintain();
				maintain.RoomId = room.RoomId;
				maintain.EmpId = room_maintain.EmpId;
				maintain.MaintainTime = room_maintain.MaintainTime;
				maintain.Object = room_maintain.Object;
				maintain.Result = room_maintain.Result;
                _context.Maintains.Add(maintain);
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

		public bool Delete(decimal room_id, decimal emp_id, DateTime maintain_time)
		{
			try
			{
				var maintain = _context.Maintains.FirstOrDefault(a => a.RoomId == room_id && a.EmpId==emp_id && a.MaintainTime== maintain_time);
				_context.Maintains.Remove(maintain);
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

		public Maintain? Get(decimal room_id, decimal emp_id, DateTime maintain_time)
		{
			try
			{
				Maintain maintain = _context.Maintains.FirstOrDefault(a => a.RoomId == room_id && a.EmpId == emp_id && a.MaintainTime == maintain_time);
				if (maintain == null)
				{
					return null;
				}
				return maintain;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Maintain> GetByRoom(decimal roomId)
        {
			try
			{
				var maintains = _context.Maintains.Where(a => a.RoomId == roomId);
				return maintains;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Maintain> GetByObject(string objectName)
        {
			try
			{
				var maintains = _context.Maintains.Where(a => a.Object == objectName);
				return maintains;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Maintain>? GetAll()
		{
            try
            {
                return _context.Maintains;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public bool Update(Maintain newMaintain)
		{
            try
            {
                var existedMaintain = _context.Maintains.FirstOrDefault(a=>a.RoomId == newMaintain.RoomId
				&& a.EmpId == newMaintain.EmpId && a.MaintainTime == newMaintain.MaintainTime);
                if (existedMaintain == null)
                {
                    Console.WriteLine("无对应的维修信息 更新失败");
                    return false;
                }
                Type maintainType = typeof(Maintain);
                PropertyInfo[] properties = maintainType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newMaintain);
                    if (value != null)
                    {
                        property.SetValue(existedMaintain, value);
                    }
                }
                _context.Maintains.Update(existedMaintain);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
	}
}

public class Room_maintain
{
    public decimal? RoomNum { get; set; }

    public decimal EmpId { get; set; }

    public DateTime MaintainTime { get; set; }

    public string? Object { get; set; }

    public string? Result { get; set; }
}
