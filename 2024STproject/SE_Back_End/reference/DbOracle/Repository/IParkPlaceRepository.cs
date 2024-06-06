using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IParkPlaceRepository
	{
		public bool Add(ParkPlace parkPlace);

		public bool Delete(decimal parking_space_id);

		public bool Update(ParkPlace parkPlace);

		public ParkPlace? Get(decimal parking_space_id);

		public IEnumerable<ParkPlace>? GetAll();
	}
}
