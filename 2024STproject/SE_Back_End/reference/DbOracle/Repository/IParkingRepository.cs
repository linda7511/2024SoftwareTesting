using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IParkingRepository
    {
        public bool Add(Parking parking);

        public bool Delete(decimal parking_space_id, string car_number, DateTime start_time);

        public bool Update(Parking parking);

        public Parking? Get(decimal parking_space_id, string car_number, DateTime start_time);


        public IEnumerable<CombinedDataOccupancy> GetByParkingAndPlace();

        public IEnumerable<CombinedDataParking> GetAll();

        public IEnumerable<CombinedDataParking> GetByCarNumber(string car_number);

        public IEnumerable<CombinedDataParking> GetByTime(DateTime earliest, DateTime latest);

        public IEnumerable<CombinedDataParking> GetByParkingPlaceType(string type);

        public IEnumerable<CombinedDataParking> GetByArea(string area);

        public IEnumerable<CombinedDataParking> GetByStatus(string status);

        public IEnumerable<CombinedDataParking> GetByPrice(decimal min_price, decimal max_price);

    }
}
