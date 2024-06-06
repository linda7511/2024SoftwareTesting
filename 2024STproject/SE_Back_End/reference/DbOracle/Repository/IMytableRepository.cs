using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IMytableRepository
	{
		public bool Add(Mytable myTable);

		public bool Delete(decimal table_id);

        public bool Update(Mytable newTable);

        public bool? UpdateStatus(decimal tableId, string newStatus);

        public Mytable? Get(decimal table_id);

        public IEnumerable<Mytable> GetLeisureTable();

        public IEnumerable<Mytable>? GetAll();
    }
}
