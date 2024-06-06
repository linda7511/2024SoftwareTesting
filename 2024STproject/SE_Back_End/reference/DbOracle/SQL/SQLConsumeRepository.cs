using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLConsumeRepository : IConsumeRepository
    {
        private MyDbContext _context;

        public SQLConsumeRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Consume consume)
        {
            try
            {
                var goods = _context.Goods.FirstOrDefault(a=>a.GoodsId== consume.GoodsId);
                if(consume.ConsumeQuantity!=null)
                    goods.Inventory-=consume.ConsumeQuantity;
                _context.Consumes.Add(consume);
                _context.Goods.Update(goods);
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

        public bool Delete(decimal DepartmentId, decimal GoodsId)
        {
            try
            {
                var consume = _context.Consumes.FirstOrDefault(a => a.DepartmentId == DepartmentId && a.GoodsId == GoodsId);
                _context.Consumes.Remove(consume);
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

        public Consume? Get(decimal DepartmentId, decimal GoodsId)
        {
            try
            {
                Consume consume = _context.Consumes.FirstOrDefault(a => a.DepartmentId == DepartmentId && a.GoodsId == GoodsId);
                if (consume == null)
                {
                    return null;
                }
                return consume;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Consume>? GetByDepartmentId(decimal DepartmentId)
        {
            try
            {
                var consumes = _context.Consumes.Where(a => a.DepartmentId == DepartmentId);
                if (consumes == null)
                {
                    return null;
                }
                return consumes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Consume>? GetByGoodsId(decimal GoodsId)
        {
            try
            {
                var consumes = _context.Consumes.Where(a => a.GoodsId == GoodsId);
                if (consumes == null)
                {
                    return null;
                }
                return consumes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Consume>? GetAll()
        {
            try
            {
                return _context.Consumes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Update(Consume newConsume)
        {
            try
            {
                var existedConsume = _context.Consumes.FirstOrDefault(a => a.DepartmentId == newConsume.DepartmentId && a.GoodsId == newConsume.GoodsId);
                if (existedConsume == null)
                {
                    Console.WriteLine("无对应的消费信息 更新失败");
                    return false;
                }
                Type consumeType = typeof(Consume);
                PropertyInfo[] properties = consumeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newConsume);
                    if (value != null)
                    {
                        property.SetValue(existedConsume, value);
                    }
                }
                _context.Consumes.Update(existedConsume);
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
