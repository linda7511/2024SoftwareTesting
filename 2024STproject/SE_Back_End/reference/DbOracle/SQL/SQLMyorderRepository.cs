using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
	public class SQLMyorderRepository : IMyorderRepository
	{
		private MyDbContext _context;

		public SQLMyorderRepository(MyDbContext context)
		{
			_context = context;
		}

		public bool Add(Myorder myOrder)
		{
			try
			{
                var mytable = _context.Mytables.FirstOrDefault(a=>a.TableId==myOrder.TableId);
                if(mytable.Bookable==0)
                    return false;
                _context.Myorders.Add(myOrder);
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

		public bool Delete(decimal table_id, decimal dish_id, DateTime order_time)
		{
			try
			{
				var myOrder = _context.Myorders.FirstOrDefault(a => a.TableId == table_id&& a.DishId == dish_id && a.OrderTime == order_time);
				_context.Myorders.Remove(myOrder);
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

		public Myorder? Get(decimal table_id, decimal dish_id, DateTime order_time)
		{
			try
			{
				Myorder myOrder = _context.Myorders.FirstOrDefault(a => a.TableId == table_id && a.DishId == dish_id && a.OrderTime == order_time);
				/*Console.WriteLine(myOrder.OrderTime);*/
				if (myOrder == null)
				{
					return null;
				}
				return myOrder;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
				return null;
			}
		}

        public IEnumerable<OrderAndDish>? GetOrderAndDishInfo(decimal tableId)
        {
            try
            {
                var data =
                    (
                    from order in _context.Myorders
                    from dish in _context.Dishes
                    where order.TableId == tableId && dish.DishId == order.DishId 
                    select new OrderAndDish
                    {
                        OrderTime = order.OrderTime,
                        OrderStatus = order.OrderStatus,
                        DishName = dish.DishName,
                        DishPrice = dish.DishPrice,
                        ConsumptionRecordId = order.ConsumptionRecordId,
                    }
                    );
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Myorder myOrder)
		{
            try
            {
                var temp = _context.Myorders.Find(myOrder.TableId, myOrder.DishId, myOrder.OrderTime);
                if (temp == null)
                {
                    Console.WriteLine("无对应的订单信息 更新失败");
                    return false;
                }
                Type dishType = typeof(Myorder);
                PropertyInfo[] properties = dishType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(myOrder);
                    if (value != null)
                    {
                        property.SetValue(temp, value);
                    }
                }
                _context.Myorders.Update(temp);
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

public class OrderAndDish
{
    public DateTime OrderTime { get; set; }

    public string? OrderStatus { get; set; }

    public string? DishName { get; set; }

    public decimal? DishPrice { get; set; }

    public decimal? ConsumptionRecordId { get; set; }
}
