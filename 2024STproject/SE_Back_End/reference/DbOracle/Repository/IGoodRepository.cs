using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IGoodRepository
	{
		public bool Add(Good good);

		public bool Delete(decimal goods_id);

		public bool Update(Good good);

		public Good? Get(decimal id);

		public Good? GetByName(string goodsName);

        public IEnumerable<Emp_Good>? GetByEmpId(decimal EmpId);

		public IEnumerable<Good>? GetAllInfo();

    }
}
