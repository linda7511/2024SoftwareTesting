using DbOracle.Models;

namespace DbOracle.Repository
{
	public interface IMaintainRepository
	{
		public bool Add(Room_maintain maintain);

		public bool Delete(decimal room_id, decimal emp_id, DateTime maintain_time);

		public bool Update(Maintain maintain);

		public Maintain? Get(decimal room_id,decimal emp_id, DateTime maintain_time);

		public IEnumerable<Maintain> GetByRoom(decimal roomId);

		public IEnumerable<Maintain> GetByObject(string objectName);

		public IEnumerable<Maintain>? GetAll();

    }
}
