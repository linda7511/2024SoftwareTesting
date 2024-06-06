using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLAttendanceInformationRepository : IAttendanceInformationRepository
    {
        private MyDbContext _context;

        public SQLAttendanceInformationRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(AttendanceInformation attendanceInformation)
        {
            try
            {
                _context.AttendanceInformations.Add(attendanceInformation);
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
                var attendanceInformation = _context.AttendanceInformations.FirstOrDefault(a => a.AttendanceId == id);
                _context.AttendanceInformations.Remove(attendanceInformation);
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

        public AttendanceInformation? Get(decimal id)
        {
            try
            {
                AttendanceInformation attendanceInformation = _context.AttendanceInformations.FirstOrDefault(a => a.AttendanceId == id);
                if (attendanceInformation == null)
                {
                    return null;
                }
                return attendanceInformation;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<AttendanceInformation>? GetByEmpId(decimal employee_id)
        {
            try
            {
                var attendanceInformations = _context.AttendanceInformations.Where(a => a.EmpId == employee_id);
                if (attendanceInformations == null)
                {
                    return null;
                }
                return attendanceInformations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(AttendanceInformation newAttendce)
        {
            try
            {
                var existedAttendce = _context.AttendanceInformations.Find(newAttendce.AttendanceId);
                if (existedAttendce == null)
                {
                    Console.WriteLine("无对应的考勤信息 更新失败");
                    return false;
                }
                Type attendanceType = typeof(AttendanceInformation);
                PropertyInfo[] properties = attendanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newAttendce);
                    if (value != null)
                    {
                        property.SetValue(existedAttendce, value);
                    }
                }
                _context.AttendanceInformations.Update(existedAttendce);
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
