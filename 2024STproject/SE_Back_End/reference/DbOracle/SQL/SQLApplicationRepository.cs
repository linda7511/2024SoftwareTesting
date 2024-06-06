using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;
using System;

namespace DbOracle.SQL
{
    public class SQLApplicationRepository : IApplicationRepository
    {
        private MyDbContext _context;

        public SQLApplicationRepository(MyDbContext context)
        {
            _context = context;
        }

        public Application Add(Application application)
        {
            try
            {
                _context.Applications.Add(application);
                _context.SaveChanges();
                return application;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
            //return true;
        }

        public bool Delete(decimal id)
        {
            try
            {
                var application = _context.Applications.FirstOrDefault(a => a.ApplicationId == id);
                _context.Applications.Remove(application);
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


        public Application? Get(decimal id)
        {
            try
            {
                Application application = _context.Applications.FirstOrDefault(a => a.ApplicationId == id);
                if (application == null)
                {
                    return null;
                }
                return application;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Application>? GetAll()
        {
            try
            {
                return _context.Applications;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Application>? GetByEmpId(decimal employeeID)
        {
            try
            {
                var applications = _context.Applications.Where(a => a.EmployeeId == employeeID);
                if (applications == null)
                {
                    return null;
                }
                return applications;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Application>? GetByType(string type)
        {
            try
            {
                var applications = _context.Applications.Where(a => a.ApplicationType == type);
                if (applications == null)
                {
                    return null;
                }
                return applications;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Application>? GetByApplicationTimeOfDay(DateTime applicationTime)
        {
            try
            {
                //2023-09-09T08:25:45.287Z
                //DateMatching(a.ApplicationTime, applicationTime)
                var applications = _context.Applications.AsEnumerable().Where(a => DateMatching(a.ApplicationTime, applicationTime) == true).ToList();
                if (applications == null)
                {
                    return null;
                }
                return applications;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Application new_application)
        {
            try
            {
                //这里是利用反射写的
                var old_application = _context.Applications.Find(new_application.ApplicationId);
                Type applicationsType = typeof(Application);
                PropertyInfo[] properties = applicationsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(new_application);
                    if (value != null)
                    {
                        property.SetValue(old_application, value);
                    }
                }
                _context.Applications.Update(old_application);
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

        static private bool DateMatching(DateTime? applicationTime, DateTime expectTime)
        {
            DateTime dateTime;
            if (applicationTime == null)
                return false;
            else
                dateTime = (DateTime)applicationTime;
            return dateTime.Year == expectTime.Year&& dateTime.Month == expectTime.Month&&dateTime.Day == expectTime.Day?true:false;
        }
    }
}
