using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IDishRepository
    {
        public bool Add(Dish dish);

        public bool Delete(decimal id);

        public bool Update(Dish dish);

        public Dish? Get(decimal id);

        public IEnumerable<Dish> GetAllDishes();

        public IEnumerable<Table_Dish>? GetEachTableDishes();
    }
}
