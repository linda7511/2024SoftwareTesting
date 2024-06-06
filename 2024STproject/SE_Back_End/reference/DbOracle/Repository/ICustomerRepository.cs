using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface ICustomerRepository
    {
        public bool Add(Customer customer);

        public bool Delete(decimal id);

        public bool Update(Customer customer);

        public Customer? Get(decimal id);

        public Customer? GetById(string id);

        public IEnumerable<Customer>? GetByName(string name);

        public IEnumerable<Customer>? GetByPhone(string phone);

        public IEnumerable<CusAndTime> GetCustomerAndTimeInfo(decimal roomNum);

        public IEnumerable<Customer>? GetAll();
    }
}
