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
	
	public class BillController : Controller
    {

        private readonly IBillRepository _billRepository;

        private readonly ILogger<BillController> _logger;
        public BillController(ILogger<BillController> logger, IBillRepository billRepository)
        {
            _logger = logger;
            _billRepository = billRepository;
        }

        [HttpGet("{id}")]
        public Bill? Get(decimal id)
        {
            return _billRepository.Get(id);
        }

        /// <summary>
        /// 账单表 返回所有账单信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Bill>? GetAll()
        {
            return _billRepository.GetAll();
        }

        /// <summary>
        /// 账单表 主码：BillId
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Bill bill)
        {
            return _billRepository.Update(bill);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _billRepository.Delete(id);
        }

        /// <summary>
        /// 不要传BillId
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        [HttpPost]
        public decimal? Add(Bill bill)
        {
            return _billRepository.Add(bill);
        }

    }
}
