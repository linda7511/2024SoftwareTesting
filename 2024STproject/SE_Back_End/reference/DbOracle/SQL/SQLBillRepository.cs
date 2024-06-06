using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLBillRepository : IBillRepository
    {
        private MyDbContext _context;

        public SQLBillRepository(MyDbContext context)
        {
            _context = context;
        }

        public decimal? Add(Bill bill)
        {
            try
            {
                _context.Bills.Add(bill);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
            return bill.BillId;
        }

        public bool Delete(decimal id)
        {
            try
            {
                var bill = _context.Bills.FirstOrDefault(a => a.BillId == id);
                _context.Bills.Remove(bill);
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

        public Bill? Get(decimal id)
        {
            try
            {
                Bill bill = _context.Bills.FirstOrDefault(a => a.BillId == id);
                if (bill == null)
                {
                    return null;
                }
                return bill;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Bill>? GetAll()
        {
            try
            {
                return _context.Bills;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Update(Bill newBill)
        {
            try
            {
                var existedBill = _context.Bills.Find(newBill.BillId);
                if (existedBill == null)
                {
                    Console.WriteLine("无对应的账单信息 更新失败");
                    return false;
                }
                Type billType = typeof(Bill);
                PropertyInfo[] properties = billType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newBill);
                    if (value != null)
                    {
                        property.SetValue(existedBill, value);
                    }
                }
                _context.Bills.Update(existedBill);
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
