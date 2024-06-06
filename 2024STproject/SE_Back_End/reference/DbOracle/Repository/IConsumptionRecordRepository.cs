using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IConsumptionRecordRepository
    {
        public bool Add(ConsumptionRecord consumptionRecord);

        public bool AddCateringRecord(CateringRecord cateringRecord);

        public bool Delete(decimal id);

        public bool Update(ConsumptionRecord consumptionRecord);

        public ConsumptionRecord? Get(decimal id);

        public IEnumerable<ConsumptionRecord> GetByRoomNum(decimal roomNum);

        public IEnumerable<ConsumptionRecord>? GetByID(string ID);
    }
}
