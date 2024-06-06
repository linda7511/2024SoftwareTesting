using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLEnterExitRepository : IEnterExitRepository
    {
        private MyDbContext _context;

        public SQLEnterExitRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(EnterExit enterExit)
        {
            try
            {
                _context.EnterExits.Add(enterExit);
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
                var enterExit = _context.EnterExits.FirstOrDefault(a => a.InfoId == id);
                _context.EnterExits.Remove(enterExit);
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


        public EnterExit? Get(decimal id)
        {
            try
            {
                EnterExit enterExit = _context.EnterExits.FirstOrDefault(a => a.InfoId == id);
                if (enterExit == null)
                {
                    return null;
                }
                return enterExit;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }


        public IEnumerable<EnterExit>? GetAll()
        {
            try
            {
                var enterExits = _context.EnterExits;
                if (enterExits == null)
                {
                    return null;
                }
                return enterExits;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }


        public bool Update(EnterExit new_enterExit)
        {
            try
            {
                var old_enterExit = _context.EnterExits.Find(new_enterExit.InfoId);
                if (old_enterExit == null)
                {
                    Console.WriteLine("无对应的出入信息 更新失败");
                    return false;
                }
                Type enterExitType = typeof(EnterExit);
                PropertyInfo[] properties = enterExitType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(new_enterExit);
                    if (value != null)
                    {
                        property.SetValue(old_enterExit, value);
                    }
                }
                _context.EnterExits.Update(old_enterExit);
                _context.SaveChanges();
                return true;
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
