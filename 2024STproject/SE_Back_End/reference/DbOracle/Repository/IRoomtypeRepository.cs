using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IRoomtypeRepository
    {
        public bool Add(Roomtype supplier);

        public bool Delete(decimal type_id);

        public Roomtype? Get(decimal type_id);

        public bool Update(Roomtype supplier);

        public IEnumerable<CombinedDataRoomtype> GetByType(string typeName);

        IEnumerable<Roomtype> GetBetween(decimal price_up, decimal price_down);

	}
}
