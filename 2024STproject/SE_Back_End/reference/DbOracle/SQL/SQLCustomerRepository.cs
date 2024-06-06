using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private MyDbContext _context;

        public SQLCustomerRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
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
                var customer = _context.Customers.FirstOrDefault(a => a.CustomerId == id);
                _context.Customers.Remove(customer);
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

        public Customer? Get(decimal id)
        {
            try
            {
                Customer customer = _context.Customers.FirstOrDefault(a => a.CustomerId == id);
                if (customer == null)
                {
                    return null;
                }
                return customer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public Customer? GetById(string id)
        {
            try
            {
                Customer customer = _context.Customers.FirstOrDefault(a => a.Id == id);
                if (customer == null)
                {
                    return null;
                }
                return customer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Customer>? GetByName(string name)
        {
            try
            {
                var customer = _context.Customers.Where(a => a.Name == name);
                if (customer == null)
                {
                    return null;
                }
                return customer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Customer>? GetByPhone(string phone)
        {
            try
            {
                var customer = _context.Customers.Where(a => a.Phone == phone);
                if (customer == null)
                {
                    return null;
                }
                return customer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<CusAndTime>? GetCustomerAndTimeInfo(decimal roomNum)
        {
            try
            {
                var data =
                    (
                    from room in _context.Rooms
                    from checkin in _context.Checkins
                    from customer in _context.Customers
                    where room.RoomNum == roomNum && checkin.RoomId == room.RoomId && customer.CustomerId == checkin.CustomerId
                    select new CusAndTime
                    {
                        CustomerId = customer.CustomerId,
                        Id = customer.Id,
                        Gender = customer.Gender,
                        Phone = customer.Phone,
                        CreditGrade = customer.CreditGrade,
                        MemberGrade = customer.MemberGrade,
                        InTime = checkin.InTime,
                        OutTime = checkin.OutTime
                    }
                    ) ;
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }  
        }

        public bool Update(Customer new_customer)
        {

            try
            {
                var old_customer = _context.Customers.Find(new_customer.CustomerId);
                Type customerType = typeof(Customer);
                PropertyInfo[] properties = customerType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(new_customer);
                    if (value != null)
                    {
                        property.SetValue(old_customer, value);
                    }
                }
                _context.Customers.Update(old_customer);
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

        public IEnumerable<Customer>? GetAll()
        {
            try
            {
                var customer = _context.Customers;
                return customer;
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

public class CusAndTime
{
    public decimal CustomerId { get; set; }

    public string? Id { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? CreditGrade { get; set; }

    public string? MemberGrade { get; set; }

    public DateTime InTime { get; set; }

    public DateTime? OutTime { get; set; }
}
