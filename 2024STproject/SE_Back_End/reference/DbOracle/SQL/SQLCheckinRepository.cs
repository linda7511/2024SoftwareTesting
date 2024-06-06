using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLCheckinRepository : ICheckinRepository
    {
        private MyDbContext _context;

        public SQLCheckinRepository(MyDbContext context)
        {
            _context = context; 
		}

        public bool Add(Checkin checkin)
        {
            try
            {
                Room room=_context.Rooms.FirstOrDefault(a=>a.RoomId==checkin.RoomId);
                room.Status = "已入住";
                _context.Rooms.Update(room);
                _context.Checkins.Add(checkin);
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

        public bool Delete(decimal CustomerId, decimal RoomId, DateTime InTime)
        {
            try
            {
                var checkin = _context.Checkins.FirstOrDefault(a => a.CustomerId == CustomerId
                && a.RoomId == RoomId && a.InTime == InTime );
                _context.Checkins.Remove(checkin);
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

        public Checkin? Get(decimal CustomerId, decimal RoomId, DateTime InTime)
        {
            try
            {
                Checkin checkin = _context.Checkins.FirstOrDefault(a => a.CustomerId == CustomerId 
                && a.RoomId == RoomId && a.InTime == InTime );
                if (checkin == null)
                {
                    return null;
                }
                return checkin;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Checkin newCheckin)
        {
            try
            {
                var existedCheckin = _context.Checkins.FirstOrDefault(a => a.CustomerId == newCheckin.CustomerId
                && a.RoomId == newCheckin.RoomId && a.InTime == newCheckin.InTime);
                if (existedCheckin == null)
                {
                    Console.WriteLine("无对应的入住信息 更新失败");
                    return false;
                }
                Type checkinType = typeof(Checkin);
                PropertyInfo[] properties = checkinType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newCheckin);
                    if (value != null)
                    {
                        property.SetValue(existedCheckin, value);
                    }
                }
                _context.Checkins.Update(existedCheckin);
                if(existedCheckin.OutTime!=null)
                {
                    Room room = _context.Rooms.FirstOrDefault(a => a.RoomId == newCheckin.RoomId);
                    room.Status = "空闲";
                    _context.Rooms.Update(room);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
		/// 入住表 通过房间号查
		/// </summary>
        public IEnumerable<Checkin>? GetByRoomNum(decimal roomNum)
        {
            try
            {
                var room = _context.Rooms.FirstOrDefault(a => a.RoomNum == roomNum);
                if (room == null)
                {
                    Console.WriteLine("无相应的房间信息 查询失败");
                    return null;
                }
                var checkins = _context.Checkins.Where(a => a.RoomId == room.RoomId);
                return checkins;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IEnumerable<Checkin>? GetByCustomerIdAndInTime(decimal customerId, DateTime inTime)
        {
            try
            {
                var checkins = _context.Checkins.Where(a => a.CustomerId == customerId&&a.InTime== inTime);
                return checkins;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IEnumerable<Checkin>? GetAll()
        {
            try
            {
                return _context.Checkins;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
