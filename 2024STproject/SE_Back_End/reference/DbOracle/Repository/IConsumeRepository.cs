using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IConsumeRepository
    {
        public bool Add(Consume consume);

        public bool Delete(decimal DepartmentId, decimal GoodsId);

        public bool Update(Consume consume);

        public Consume? Get(decimal DepartmentId, decimal GoodsId);

        public IEnumerable<Consume>? GetByDepartmentId(decimal DepartmentId);

        public IEnumerable<Consume>? GetByGoodsId(decimal GoodsId);

        public IEnumerable<Consume>? GetAll();
    }
}
