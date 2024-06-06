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
	
	public class ConsumptionRecordController : Controller
    {

        private readonly IConsumptionRecordRepository _consumptionRecordRepository;

        private readonly ILogger<ConsumptionRecordController> _logger;
        public ConsumptionRecordController(ILogger<ConsumptionRecordController> logger, IConsumptionRecordRepository consumptionRecordRepository)
        {
            _logger = logger;
            _consumptionRecordRepository = consumptionRecordRepository;
        }

        [HttpGet("{id}")]
        public ConsumptionRecord? Get(decimal id)
        {
            return _consumptionRecordRepository.Get(id);
        }

        /// <summary>
        /// 消费记录表 主码：ConsumeId
        /// </summary>
        /// <param name="consumptionRecord"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(ConsumptionRecord consumptionRecord)
        {
            return _consumptionRecordRepository.Update(consumptionRecord);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _consumptionRecordRepository.Delete(id);
        }

        /// <summary>
        /// 不要传ConsumeId
        /// </summary>
        /// <param name="consumptionRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(ConsumptionRecord consumptionRecord)
        {
            return _consumptionRecordRepository.Add(consumptionRecord);
        }

        /// <summary>
        /// 消费记录表 添加有关餐饮的消费记录
        /// </summary>
        /// <param name="consumptionRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddCateringRecord(CateringRecord cateringRecord)
        {
            return _consumptionRecordRepository.AddCateringRecord(cateringRecord);
        }

        /// <summary>
		/// 消费记录表 通过房间号查询（若干条）
		/// </summary>
		[HttpGet("{roomNum}")]
        public IEnumerable<ConsumptionRecord> GetByRoomNum(decimal roomNum)
        {
            return _consumptionRecordRepository.GetByRoomNum(roomNum);
        }

        /// <summary>
		/// 消费记录表 通过客人身份证号查询（若干条）,注意这里的ID是身份证号，不是表主码
		/// </summary>
		[HttpGet("{ID}")]
        public IEnumerable<ConsumptionRecord>? GetByID(string ID)
        {
            return _consumptionRecordRepository.GetByID(ID);
        }
    }
}
