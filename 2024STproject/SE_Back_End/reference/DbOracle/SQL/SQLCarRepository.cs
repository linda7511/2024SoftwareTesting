using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLCarRepository : ICarRepository
    {
        private MyDbContext _context;

        public SQLCarRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Car car)
        {
            try
            {
                _context.Cars.Add(car);
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

        public bool Delete(string id)
        {
            try
            {
                var car = _context.Cars.FirstOrDefault(a => a.CarNumber == id);
                _context.Cars.Remove(car);
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

        public Car? Get(string id)
        {
            try
            {
                Car car = _context.Cars.FirstOrDefault(a => a.CarNumber == id);
                if (car == null)
                {
                    return null;
                }
                return car;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Car>? GetAll()
        {
            try
            {
                var cars = _context.Cars;
                if (cars == null)
                {
                    return null;
                }
                return cars;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public Car? GetByCarNumber(string carNumber)
        {
            try
            {
                var car = _context.Cars.FirstOrDefault(a => a.CarNumber == carNumber);
                if (car == null)
                {
                    return null;
                }
                return car;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Car newCar)
        {
            try
            {
                var existedCar = _context.Cars.Find(newCar.CarNumber);
                if (existedCar == null)
                {
                    Console.WriteLine("无对应的车辆信息 更新失败");
                    return false;
                }
                Type carType = typeof(Car);
                PropertyInfo[] properties = carType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newCar);
                    if (value != null)
                    {
                        property.SetValue(existedCar, value);
                    }
                }
                _context.Cars.Update(existedCar);
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
