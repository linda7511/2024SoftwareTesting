using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IEnterExitRepository
	{
		public bool Add(EnterExit enterExit);

		public bool Delete(decimal info_id);

		public bool Update(EnterExit enterExit);

		public EnterExit? Get(decimal info_id);

		public IEnumerable<EnterExit>? GetAll();

    }
}
