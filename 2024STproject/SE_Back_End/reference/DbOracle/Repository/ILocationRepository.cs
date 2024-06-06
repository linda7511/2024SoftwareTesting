using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface ILocationRepository
	{
		public bool Add(Location location);

		public bool Delete(decimal location_id);

		public bool Update(Location location);

		public Location? Get(decimal location_id);
	}
}
