using DbOracle.Models;
using System;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLDishRepository : IDishRepository
    {
        private MyDbContext _context;

        public SQLDishRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Dish dish)
        {
            try
            {
                _context.Dishes.Add(dish);
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
                var dish = _context.Dishes.FirstOrDefault(a => a.DishId == id);
                _context.Dishes.Remove(dish);
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

        public Dish? Get(decimal id)
        {
            try
            {
                Dish dish = _context.Dishes.FirstOrDefault(a => a.DishId == id);
                return dish;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Dish> GetAllDishes()
        {
            return _context.Dishes;
        }

        public IEnumerable<Table_Dish>? GetEachTableDishes()
        {
            try
            {
                var result =
                     (
                     from myTable in _context.Mytables
                     select new Table_Dish
                     {
                         TableId = myTable.TableId,
                         dishes = (from order in _context.Myorders
                                   from dish in _context.Dishes
                                   where order.TableId == myTable.TableId && dish.DishId == order.DishId
                                   select dish).ToList()// 使用ToList()方法将结果转换成List<Dish>
                     }
                     ); 
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Dish dish)
        {
            try
            {
                var test = _context.Dishes.Find(dish.DishId);
                if (test == null)
                {
                    Console.WriteLine("无对应的菜品信息 更新失败");
                    return false;
                }
                Type dishType = typeof(Dish);
                PropertyInfo[] properties = dishType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(dish);
                    if (value != null)
                    {
                        property.SetValue(test, value);
                    }
                }
                _context.Dishes.Update(test);
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

public class Table_Dish
{
    public decimal TableId { get; set; }

    public IEnumerable<Dish>? dishes { get; set; }
}
