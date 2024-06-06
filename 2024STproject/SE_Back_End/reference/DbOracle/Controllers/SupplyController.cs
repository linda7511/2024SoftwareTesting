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
	
	public class SupplyController : Controller
	{

		private readonly ISupplyRepository _supplyRepository;

		private readonly ILogger<SupplyController> _logger;
		public SupplyController(ILogger<SupplyController> logger, ISupplyRepository supplyRepository)
		{
			_logger = logger;
			_supplyRepository = supplyRepository;
		}

		[HttpGet("{supplier_id}/{goods_id}")]
		public Supply? Get(decimal supplier_id, decimal goods_id)
		{
			return _supplyRepository.Get(supplier_id, goods_id);
		}

		[HttpPut]
		public bool Update(Supply supply)
		{
			return _supplyRepository.Update(supply);
		}

		[HttpDelete("{supplier_id}/{goods_id}")]
		public bool Delete(decimal supplier_id, decimal goods_id)
		{
			return _supplyRepository.Delete(supplier_id, goods_id);
		}

		[HttpPost]
		public bool Add(Supply supply)
		{
			return _supplyRepository.Add(supply);
		}

		/// <summary>
		/// 供应表 增 如果没有对应的供应商则先增加供应商表
		/// </summary>
		/// <param name="supply_supplier"></param>
		/// <returns></returns>
        [HttpPost]
        public bool AddSupplySupplier(Supply_Supplier supply_supplier)
		{
			return _supplyRepository.AddSupplySupplier(supply_supplier);
        }
    }
}
