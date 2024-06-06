using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLRoomorderRepository : IRoomorderRepository
	{
		private MyDbContext _context;

		public SQLRoomorderRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Roomorder roomOrder)
		{
			try
			{
				_context.Roomorders.Add(roomOrder);
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
				var roomOrder = _context.Roomorders.FirstOrDefault(a => a.OrderId == id);
				_context.Roomorders.Remove(roomOrder);
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

		public Roomorder? Get(decimal id)
		{
			try
			{
				var roomOrder = _context.Roomorders.FirstOrDefault(a => a.CustomerId == id);
				if (roomOrder == null)
				{
					return null;
				}
				return roomOrder;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Roomorder>? GetAll()
        {
			try
			{
				var roomOrders = _context.Roomorders;
				return roomOrders;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

		public IEnumerable<Roomorder>? GetByCustomerId(decimal CustomerId)
        {
			try
			{
				var roomOrders = _context.Roomorders.Where(a=>a.CustomerId== CustomerId);
				return roomOrders;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}
		public bool Update(Roomorder roomOrder)
		{
            try
            {
                var temp = _context.Roomorders.Find(roomOrder.OrderId);
                if (temp == null)
                {
                    Console.WriteLine("无对应的订单信息 更新失败");
                    return false;
                }
                Type dishType = typeof(Roomorder);
                PropertyInfo[] properties = dishType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(roomOrder);
                    if (value != null)
                    {
                        property.SetValue(temp, value);
                    }
                }
                _context.Roomorders.Update(temp);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
		}
	}
}
