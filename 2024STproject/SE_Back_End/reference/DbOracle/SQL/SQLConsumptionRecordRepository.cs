using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLConsumptionRecordRepository : IConsumptionRecordRepository
    {
        private MyDbContext _context;

        public SQLConsumptionRecordRepository(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 消费记录表 根据房间ID、员工ID、消费种类和金额 增
        /// </summary>
        /// <param name="consumptionRecord"></param>
        /// <returns></returns>
        public bool Add(ConsumptionRecord consumptionRecord)
        {
            try
            {
                _context.ConsumptionRecords.Add(consumptionRecord);
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

        public bool AddCateringRecord(CateringRecord cateringRecord)
        {
            try
            {
                ConsumptionRecord consumptionRecord = new ConsumptionRecord();
                decimal roomId= _context.Rooms.FirstOrDefault(a=>a.RoomNum== cateringRecord.RoomNum).RoomId;

                consumptionRecord.RoomId = roomId;
                consumptionRecord.EmployeeId= cateringRecord.EmployeeId;
                consumptionRecord.ConsumeType = "餐饮费";
                consumptionRecord.ConsumeTime = DateTime.Now;
                consumptionRecord.ConsumeAmount = cateringRecord.SumCost;
                _context.ConsumptionRecords.Add(consumptionRecord);
                _context.SaveChanges();//保存之后consumptionRecord的id就自动填上了

                Mytable mytable = _context.Mytables.FirstOrDefault(a=>a.TableId== cateringRecord.TableId);
                mytable.TableStatus = "空闲";
                _context.Mytables.Update(mytable);

                foreach(OrderMessage orderMessage in cateringRecord.orderMessages)
                {
                    Myorder myorder = _context.Myorders.FirstOrDefault(a=>a.TableId== orderMessage.TableId &&
                                        a.DishId == orderMessage.DishId && a.OrderTime == orderMessage.OrderTime);
                    myorder.ConsumptionRecordId = consumptionRecord.ConsumeId;
                    _context.Myorders.Update(myorder);
                }
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
                var consumptionRecord = _context.ConsumptionRecords.FirstOrDefault(a => a.ConsumeId == id);
                _context.ConsumptionRecords.Remove(consumptionRecord);
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

        public ConsumptionRecord? Get(decimal id)
        {
            try
            {
                ConsumptionRecord consumptionRecord = _context.ConsumptionRecords.FirstOrDefault(a => a.ConsumeId == id);
                if (consumptionRecord == null)
                {
                    return null;
                }
                return consumptionRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(ConsumptionRecord newConsumptionRecord)
        {
            try
            {
                var existedConsumptionRecord = _context.ConsumptionRecords.Find(newConsumptionRecord.ConsumeId);
                if (existedConsumptionRecord == null)
                {
                    Console.WriteLine("无对应的消费记录信息 更新失败");
                    return false;
                }
                Type consumptionRecordType = typeof(ConsumptionRecord);
                PropertyInfo[] properties = consumptionRecordType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newConsumptionRecord);
                    if (value != null)
                    {
                        property.SetValue(existedConsumptionRecord, value);
                    }
                }
                _context.ConsumptionRecords.Update(existedConsumptionRecord);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// 消费记录表 通过房间号查
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public IEnumerable<ConsumptionRecord> GetByRoomNum(decimal roomNum)
        {
            try
            {
                var room = _context.Rooms.FirstOrDefault(a=>a.RoomNum== roomNum);
                var consumptionRecords = _context.ConsumptionRecords.Where(a => a.RoomId == room.RoomId);
                return consumptionRecords;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        /// <summary>
        /// 消费记录表 通过客人身份证号查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ConsumptionRecord> GetByID(string ID)
        {
            try
            {
                var data =
                    (
                    from t1 in _context.Customers
                    from t2 in _context.Checkins
                    from t3 in _context.ConsumptionRecords
                    where t1.Id==ID&&t1.CustomerId==t2.CustomerId&&t2.RoomId==t3.RoomId
                    select _context.ConsumptionRecords
                    );

                return (IEnumerable<ConsumptionRecord>)data;
                /*
                Customer customer = _context.Customers.FirstOrDefault(a => a.Id == ID);
                Checkin checkin = _context.Checkins.FirstOrDefault(a => a.CustomerId == customer.CustomerId);
                var consumptionRecords = _context.ConsumptionRecords.Where(a => a.RoomId == checkin.RoomId);
                return consumptionRecords;
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }
    }
}

public class CateringRecord
{
    public decimal RoomNum { get; set; }
    public decimal SumCost{ get; set; }
    public decimal TableId{ get; set; }
    public decimal EmployeeId { get; set; }
    public OrderMessage[] orderMessages { get; set; }
}

public class OrderMessage
{
    public decimal TableId { get; set; }
    public decimal DishId { get; set; }
    public DateTime OrderTime { get; set; }
}