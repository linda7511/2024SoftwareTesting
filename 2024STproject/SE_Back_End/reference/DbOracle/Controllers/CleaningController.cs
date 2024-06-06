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
	
	public class CleaningController : Controller
    {

        private readonly ICleaningRepository _cleaningRepository;

        private readonly ILogger<CleaningController> _logger;
        public CleaningController(ILogger<CleaningController> logger, ICleaningRepository cleaningRepository)
        {
            _logger = logger;
            _cleaningRepository = cleaningRepository;
        }

        [HttpGet("{RoomId}/{EmpId}/{CleaningTime}")]
        public Cleaning? Get(decimal RoomId, decimal EmpId,DateTime CleaningTime)
        {
            return _cleaningRepository.Get(RoomId, EmpId, CleaningTime);
        }

        /// <summary>
        /// 清洁表 通过RoomId查找清洁记录
        /// </summary>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        [HttpGet("{RoomId}")]
        public IEnumerable< Cleaning>? GetByRoomId(decimal RoomId)
        {
            return _cleaningRepository.GetByRoomId(RoomId);
        }

        /// <summary>
        /// 清洁表 通过EmpId查找清洁记录
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        [HttpGet("{EmpId}")]
        public IEnumerable<Cleaning>? GetByEmpId( decimal EmpId)
        {
            return _cleaningRepository.GetByEmpId(EmpId);
        }

        /// <summary>
        /// 主码：RoomId、EmpId、CleaningTime
        /// </summary>
        /// <param name="cleaning"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Cleaning cleaning)
        {
            return _cleaningRepository.Update(cleaning);
        }

        [HttpDelete("{RoomId}/{EmpId}/{CleaningTime}")]
        public bool Delete(decimal RoomId, decimal EmpId,DateTime CleaningTime)
        {
            return _cleaningRepository.Delete(RoomId, EmpId, CleaningTime);
        }

        /// <summary>
        /// 清洁表 增
        /// </summary>
        /// <param name="room_Cleaning"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(Room_Cleaning room_Cleaning)
        {
            return _cleaningRepository.Add(room_Cleaning);
        }
    }
}
