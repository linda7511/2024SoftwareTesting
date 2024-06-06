using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface ICheckrepairRepository
    {
        public bool Add(Checkrepair checkrepair);

        public bool Delete(decimal EmpId, decimal AssetId,DateTime CheckRepairTime);

        public bool Update(Checkrepair checkrepair);

        public Checkrepair? Get(decimal EmpId, decimal AssetId,DateTime  CheckRepairTime);

        public IEnumerable<Checkrepair>? GetByEmpId(decimal EmpId);

        public IEnumerable<Checkrepair>? GetByAssetId(decimal AssetId);
    }
}
