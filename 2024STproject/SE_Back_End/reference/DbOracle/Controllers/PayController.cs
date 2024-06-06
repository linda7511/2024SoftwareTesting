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
	
	public class PayController : Controller
	{

		private readonly IPayRepository _payRepository;

		private readonly ILogger<PayController> _logger;
		public PayController(ILogger<PayController> logger, IPayRepository payRepository)
		{
			_logger = logger;
			_payRepository = payRepository;
		}

		[HttpGet("{customer_id}/{bill_id}")]
		public Pay? Get(decimal customer_id, decimal bill_id)
		{
			return _payRepository.Get(customer_id, bill_id);
		}

		/// <summary>
		/// 支付表 获取全部支付记录（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable< Pay>? GetAll()
		{
			return _payRepository.GetAll();
		}

		/// <summary>
		/// 支付表 主码：CustomerId、BillId
		/// </summary>
		/// <param name="pay"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Pay pay)
		{
			return _payRepository.Update(pay);
		}

		[HttpDelete("{customer_id}/{bill_id}")]
		public bool Delete(decimal customer_id, decimal bill_id)
		{
			return _payRepository.Delete(customer_id, bill_id);
		}

		/// <summary>
		/// 支付表 增
		/// </summary>
		/// <param name="pay"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Pay pay)
		{
			return _payRepository.Add(pay);
		}

	}
}
