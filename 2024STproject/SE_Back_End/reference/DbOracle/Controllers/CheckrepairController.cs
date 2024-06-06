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
	
	public class CheckrepairController : Controller
    {

        private readonly ICheckrepairRepository _checkRepairRepository;

        private readonly ILogger<CheckrepairController> _logger;
        public CheckrepairController(ILogger<CheckrepairController> logger, ICheckrepairRepository checkRepairRepository)
        {
            _logger = logger;
            _checkRepairRepository = checkRepairRepository;
        }

        [HttpGet("{EmpId}/{AssetId}/{CheckRepairTime}")]
        public Checkrepair? Get(decimal EmpId, decimal AssetId,DateTime CheckRepairTime)
        {
            return _checkRepairRepository.Get(EmpId, AssetId, CheckRepairTime);
        }

        /// <summary>
        /// 检修表 通过员工id查（若干条）
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        [HttpGet("{EmpId}")]
        public IEnumerable<Checkrepair>? GetByEmpId(decimal EmpId)
        {
            return _checkRepairRepository.GetByEmpId(EmpId);
        }


        /// <summary>
        /// 检修表 通过资产id查（若干条）
        /// </summary>
        /// <param name="AssetId"></param>
        /// <returns></returns>
        [HttpGet("{AssetId}")]
        public IEnumerable< Checkrepair>? GetByAssetId(decimal AssetId)
        {
            return _checkRepairRepository.GetByAssetId(AssetId);
        }

        /// <summary>
        /// 检修表 主码：EmpId、AssetId、CheckRepairTime
        /// </summary>
        /// <param name="checkRepair"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Checkrepair checkRepair)
        {
            return _checkRepairRepository.Update(checkRepair);
        }

        [HttpDelete("{EmpId}/{AssetId}/{CheckRepairTime}")]
        public bool Delete(decimal EmpId, decimal AssetId, DateTime CheckRepairTime)
        {
            return _checkRepairRepository.Delete(EmpId, AssetId, CheckRepairTime);
        }

        [HttpPost]
        public bool Add(Checkrepair checkRepair)
        {
            return _checkRepairRepository.Add(checkRepair);
        }

    }
}
