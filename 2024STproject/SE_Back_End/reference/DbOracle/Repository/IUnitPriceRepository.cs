using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IUnitPriceRepository
	{
		public bool Add(UnitPrice unitPrice);

		public bool Delete(string parking_place_type);

		public bool Update(UnitPrice unitPrice);

		public UnitPrice? Get(string parking_place_type);

		public IEnumerable<CombinedDataParkingPrice>? GetAll();

    }
}
