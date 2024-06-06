using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLCheckInCheckOutRepository : ICheckInCheckOutRepository
    {
        private MyDbContext _context;

        public SQLCheckInCheckOutRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(CheckInCheckOut checkInCheckOut)
        {
            try
            {
                _context.CheckInCheckOuts.Add(checkInCheckOut);
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
                var checkInCheckOut = _context.CheckInCheckOuts.FirstOrDefault(a => a.CheckInId == id);
                _context.CheckInCheckOuts.Remove(checkInCheckOut);
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

        public CheckInCheckOut? Get(decimal id)
        {
            try
            {
                CheckInCheckOut checkInCheckOut = _context.CheckInCheckOuts.FirstOrDefault(a => a.CheckInId == id);
                if (checkInCheckOut == null)
                {
                    return null;
                }
                return checkInCheckOut;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(CheckInCheckOut newCheckInCheckOut)
        {
            try
            {
                var existedCheckInCheckOut = _context.CheckInCheckOuts.Find(newCheckInCheckOut.CheckInId);
                if (existedCheckInCheckOut == null)
                {
                    Console.WriteLine("无对应的打卡信息 更新失败");
                    return false;
                }
                Type checkInCheckOutType = typeof(CheckInCheckOut);
                PropertyInfo[] properties = checkInCheckOutType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newCheckInCheckOut);
                    if (value != null)
                    {
                        property.SetValue(existedCheckInCheckOut, value);
                    }
                }
                _context.CheckInCheckOuts.Update(existedCheckInCheckOut);
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
