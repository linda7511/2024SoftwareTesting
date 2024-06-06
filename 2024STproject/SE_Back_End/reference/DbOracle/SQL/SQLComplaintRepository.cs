using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLComplaintRepository : IComplaintRepository
    {
        private MyDbContext _context;

        public SQLComplaintRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Complaint complaint)
        {
            try
            {
                _context.Complaints.Add(complaint);
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
                var complaint = _context.Complaints.FirstOrDefault(a => a.ComplaintId == id);
                _context.Complaints.Remove(complaint);
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

        public Complaint? Get(decimal id)
        {
            try
            {
                Complaint complaint = _context.Complaints.FirstOrDefault(a => a.ComplaintId == id);
                if (complaint == null)
                {
                    return null;
                }
                return complaint;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Complaint newComplaint)
        {
            try
            {
                var existedComplaint = _context.Complaints.Find(newComplaint.ComplaintId);
                if (existedComplaint == null)
                {
                    Console.WriteLine("无对应的投诉信息 更新失败");
                    return false;
                }
                Type complaintType = typeof(Complaint);
                PropertyInfo[] properties = complaintType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newComplaint);
                    if (value != null)
                    {
                        property.SetValue(existedComplaint, value);
                    }
                }
                _context.Complaints.Update(existedComplaint);
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
