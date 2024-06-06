using DbOracle.Models;
using DbOracle.Repository;

namespace DbOracle.SQL
{
    public class SQLCleaningRepository : ICleaningRepository
    {
        private MyDbContext _context;

        public SQLCleaningRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Room_Cleaning room_Cleaning)
        {
            try
            {
                var room = _context.Rooms.FirstOrDefault(r => r.RoomNum == room_Cleaning.RoomNum);
                if(room == null)
                {
                    Console.WriteLine("无对应房间信息，查询失败");
                    return false;
                }
                Cleaning cleaning = new Cleaning();
                cleaning.RoomId = room.RoomId;
                cleaning.EmpId = room_Cleaning.EmpId;
                cleaning.CleaningTime = room_Cleaning.CleaningTime;
                _context.Cleanings.Add(cleaning);
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

        public bool Delete(decimal RoomId, decimal EmpId, DateTime CleaningTime)
        {
            try
            {
                var cleaning = _context.Cleanings.FirstOrDefault(a => a.RoomId == RoomId && a.EmpId == EmpId&& a.CleaningTime== CleaningTime);
                _context.Cleanings.Remove(cleaning);
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

        public Cleaning? Get(decimal RoomId, decimal EmpId)
        {
            try
            {
                Cleaning cleaning = _context.Cleanings.FirstOrDefault(a => a.RoomId == RoomId && a.EmpId == EmpId);
                if (cleaning == null)
                {
                    return null;
                }
                return cleaning;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public Cleaning? Get(decimal RoomId, decimal EmpId, DateTime CleaningTime)
        {
            try
            {
                Cleaning cleaning = _context.Cleanings.FirstOrDefault(a => a.RoomId == RoomId && a.EmpId == EmpId&& a.CleaningTime== CleaningTime);
                if (cleaning == null)
                {
                    return null;
                }
                return cleaning;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Cleaning>? GetByRoomId(decimal RoomId)
        {
            try
            {
                var cleanings = _context.Cleanings.Where(a => a.RoomId == RoomId );
                if (cleanings == null)
                {
                    return null;
                }
                return cleanings;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Cleaning>? GetByEmpId(decimal EmpId)
        {
            try
            {
                var cleanings = _context.Cleanings.Where(a => a.EmpId == EmpId);
                if (cleanings == null)
                {
                    return null;
                }
                return cleanings;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Cleaning cleaning)
        {
            try
            {
                _context.Cleanings.Update(cleaning);
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
    }
}

public class Room_Cleaning
{
    public decimal? RoomNum { get; set; }

    public decimal EmpId { get; set; }

    public DateTime CleaningTime { get; set; }
}
