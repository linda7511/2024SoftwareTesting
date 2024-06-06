using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IAttendanceInformationRepository
    {
        public bool Add(AttendanceInformation attendanceInformation);

        public bool Delete(decimal id);

        public bool Update(AttendanceInformation attendanceInformation);

        public AttendanceInformation? Get(decimal id);

        public IEnumerable<AttendanceInformation>? GetByEmpId(decimal employee_id);
    }
}
