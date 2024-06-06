using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IStagingRepository
	{
		public bool Add(Staging staging);

		public bool Delete(decimal luggage_id);

		public bool Update(Staging staging);

		public Staging? Get(decimal luggage_id);
	}
}
