using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface ICarRepository
    {
        public bool Add(Car car);

        public bool Delete(string id);

        public bool Update(Car car);

        public Car? Get(string id);

        public IEnumerable<Car>? GetAll();

        public Car? GetByCarNumber(string carNumber);
    }
}
