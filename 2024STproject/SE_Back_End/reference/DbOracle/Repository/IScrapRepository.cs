using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IScrapRepository
	{
		public bool Add(Scrap scrap);

		public bool Delete(decimal emp_id,decimal asset_id);

		public bool Update(Scrap scrap);

		public Scrap? Get(decimal emp_id, decimal asset_id);
	}
}
