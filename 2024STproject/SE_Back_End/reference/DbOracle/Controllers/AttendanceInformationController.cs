using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DbOracle.Controllers;
using Microsoft.AspNetCore.Cors;
using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DbOracle.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
	
	public class AttendanceInformationController : Controller
    {

        private readonly IAttendanceInformationRepository _attendanceInformationRepository;

        private readonly ILogger<AttendanceInformationController> _logger;
        public AttendanceInformationController(ILogger<AttendanceInformationController> logger, IAttendanceInformationRepository attendanceInformationRepository)
        {
            _logger = logger;
            _attendanceInformationRepository = attendanceInformationRepository;
        }

        [HttpGet("{id}")]
        public AttendanceInformation? Get(decimal id)
        {
            return _attendanceInformationRepository.Get(id);
        }

        /// <summary>
        /// 考勤信息表 根据员工ID查（若干条）
        /// </summary>
        /// <param name="employee_id"></param>
        /// <returns></returns>
        [HttpGet("{employee_id}")]
        public IEnumerable< AttendanceInformation>? GetByEmpId(decimal employee_id)
        {
            return _attendanceInformationRepository.GetByEmpId(employee_id);
        }

        /// <summary>
        /// 出勤表 主码：AttendanceId
        /// </summary>
        /// <param name="attendanceInformation"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(AttendanceInformation attendanceInformation)
        {
            return _attendanceInformationRepository.Update(attendanceInformation);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _attendanceInformationRepository.Delete(id);
        }

        /// <summary>
        /// 不要传AttendanceId
        /// </summary>
        /// <param name="attendanceInformation"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(AttendanceInformation attendanceInformation)
        {
            return _attendanceInformationRepository.Add(attendanceInformation);
        }

    }
}
