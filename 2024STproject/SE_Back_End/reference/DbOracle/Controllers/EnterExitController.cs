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
	
	public class EnterExitController : Controller
    {

        private readonly IEnterExitRepository _enterExitRepository;

        private readonly ILogger<EnterExitController> _logger;
        public EnterExitController(ILogger<EnterExitController> logger, IEnterExitRepository enterExitRepository)
        {
            _logger = logger;
            _enterExitRepository = enterExitRepository;
        }

        [HttpGet("{id}")]
        public EnterExit? Get(decimal id)
        {
            return _enterExitRepository.Get(id);
        }

        /// <summary>
        /// 出入信息表 查所有出入信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<EnterExit>? GetAll()
        {
            return _enterExitRepository.GetAll();
        }

        /// <summary>
        /// 出入信息表 主码：InfoId
        /// </summary>
        /// <param name="enterExit"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(EnterExit enterExit)
        {
            return _enterExitRepository.Update(enterExit);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _enterExitRepository.Delete(id);
        }

        /// <summary>
        /// 不要传InfoId
        /// </summary>
        /// <param name="enterExit"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(EnterExit enterExit)
        {
            return _enterExitRepository.Add(enterExit);
        }
    }
}
