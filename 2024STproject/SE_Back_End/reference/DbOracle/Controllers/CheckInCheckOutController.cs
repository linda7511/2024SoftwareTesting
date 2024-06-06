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
	
	public class CheckInCheckOutController : Controller
    {

        private readonly ICheckInCheckOutRepository _checkInCheckOutRepository;

        private readonly ILogger<CheckInCheckOutController> _logger;
        public CheckInCheckOutController(ILogger<CheckInCheckOutController> logger, ICheckInCheckOutRepository checkInCheckOutRepository)
        {
            _logger = logger;
            _checkInCheckOutRepository = checkInCheckOutRepository;
        }

        [HttpGet("{id}")]
        public CheckInCheckOut? Get(decimal id)
        {
            return _checkInCheckOutRepository.Get(id);
        }

        /// <summary>
        /// 打卡信息表 主码：CheckInId
        /// </summary>
        /// <param name="checkInCheckOut"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(CheckInCheckOut checkInCheckOut)
        {
            return _checkInCheckOutRepository.Update(checkInCheckOut);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _checkInCheckOutRepository.Delete(id);
        }

        /// <summary>
        /// 打卡信息表 增
        /// </summary>
        /// <param name="checkInCheckOut"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(CheckInCheckOut checkInCheckOut)
        {
            return _checkInCheckOutRepository.Add(checkInCheckOut);
        }

    }
}
