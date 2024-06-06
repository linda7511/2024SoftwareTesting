using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLCheckrepairRepository : ICheckrepairRepository
    {
        private MyDbContext _context;

        public SQLCheckrepairRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Checkrepair checkRepair)
        {
            try
            {
                _context.Checkrepairs.Add(checkRepair);
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

        public bool Delete(decimal EmpId, decimal AssetId,DateTime CheckRepairTime)
        {
            try
            {
                var checkRepair = _context.Checkrepairs.FirstOrDefault(a => a.EmpId == EmpId && a.AssetId == AssetId&& a.CheckRepairTime== CheckRepairTime);
                _context.Checkrepairs.Remove(checkRepair);
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

        public Checkrepair? Get(decimal EmpId, decimal AssetId,DateTime  CheckRepairTime)
        {
            try
            {
                Checkrepair checkRepair = _context.Checkrepairs.FirstOrDefault(a => a.EmpId == EmpId && a.AssetId == AssetId&& a.CheckRepairTime== CheckRepairTime);
                if (checkRepair == null)
                {
                    return null;
                }
                return checkRepair;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Checkrepair>? GetByEmpId(decimal EmpId)
        {
            try
            {
                var checkRepairs = _context.Checkrepairs.Where(a => a.EmpId == EmpId);
                if (checkRepairs == null)
                {
                    return null;
                }
                return checkRepairs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Checkrepair>? GetByAssetId(decimal AssetId)
        {
            try
            {
                var checkRepairs = _context.Checkrepairs.Where(a => a.AssetId == AssetId);
                if (checkRepairs == null)
                {
                    return null;
                }
                return checkRepairs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Checkrepair newCheckrepair)
        {
            try
            {
                var existedCheckrepair = _context.Checkrepairs.FirstOrDefault(a => a.EmpId == newCheckrepair.EmpId && a.AssetId == newCheckrepair.AssetId&&a.CheckRepairTime==newCheckrepair.CheckRepairTime);
                if (existedCheckrepair == null)
                {
                    Console.WriteLine("无对应的检修信息 更新失败");
                    return false;
                }
                Type checkRepairType = typeof(Checkrepair);
                PropertyInfo[] properties = checkRepairType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newCheckrepair);
                    if (value != null)
                    {
                        property.SetValue(existedCheckrepair, value);
                    }
                }
                _context.Checkrepairs.Update(existedCheckrepair);
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
