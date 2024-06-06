using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IBillRepository
    {
        public decimal? Add(Bill bill);

        public bool Delete(decimal id);

        public bool Update(Bill bill);

        public Bill? Get(decimal id);

        public IEnumerable<Bill>? GetAll();
    }
}
