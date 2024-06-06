using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLServiceRepository : IServiceRepository
	{
		private MyDbContext _context;

		public SQLServiceRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Service service)
		{
			try
			{
				_context.Services.Add(service);
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

		public bool Delete(decimal customer_id, decimal emp_id, DateTime service_time)
		{
			try
			{
				var service = _context.Services.FirstOrDefault(a => a.CustomerId == customer_id&&a.EmpId== emp_id&&a.ServiceTime== service_time);
				_context.Services.Remove(service);
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

		public Service? Get(decimal customer_id, decimal emp_id, DateTime service_time)
		{
			try
			{
				Service service = _context.Services.FirstOrDefault(a => a.CustomerId == customer_id && a.EmpId == emp_id && a.ServiceTime == service_time);
				if (service == null)
				{
					return null;
				}
				return service;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public bool Update(Service newService)
		{
            try
            {
                var existedService = _context.Services.FirstOrDefault(a=>a.CustomerId == newService.CustomerId
				&& a.EmpId == newService.EmpId && a.ServiceTime == newService.ServiceTime);
                if (existedService == null)
                {
                    Console.WriteLine("无对应的服务信息 更新失败");
                    return false;
                }
                Type serviceType = typeof(Service);
                PropertyInfo[] properties = serviceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newService);
                    if (value != null)
                    {
                        property.SetValue(existedService, value);
                    }
                }
                _context.Services.Update(existedService);
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
