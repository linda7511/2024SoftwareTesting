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
	
	public class PurchaseController : Controller
	{

		private readonly IPurchaseRepository _purchaseRepository;

		private readonly ILogger<PurchaseController> _logger;
		public PurchaseController(ILogger<PurchaseController> logger, IPurchaseRepository purchaseRepository)
		{
			_logger = logger;
			_purchaseRepository = purchaseRepository;
		}

		[HttpGet("{purchase_id}")]
		public Purchase? Get(decimal purchase_id)
		{
			return _purchaseRepository.Get(purchase_id);
		}

		/// <summary>
		/// 采购表 通过货物id、员工id、供应商id查（若干条）
		/// </summary>
		/// <param name="goods_id"></param>
		/// <param name="employee_id"></param>
		/// <param name="supplier_id"></param>
		/// <returns></returns>
		[HttpGet("{goods_id}/{employee_id}/{supplier_id}")]
		public IEnumerable<Purchase>? GetByGoodEmpSupplier(decimal goods_id, decimal employee_id,decimal supplier_id)
		{
			return _purchaseRepository.GetByGoodEmpSupplier(goods_id, employee_id,supplier_id);
		}

		/// <summary>
		/// 采购表 通过物资id查（若干条）
		/// </summary>
		/// <param name="goods_id"></param>
		/// <returns></returns>
		[HttpGet("{goods_id}")]
		public IEnumerable< Purchase>? GetByGoodsId(decimal goods_id)
		{
			return _purchaseRepository.GetByGoodsId(goods_id);
		}

		/// <summary>
		/// 采购表 通过员工id查（若干条）
		/// </summary>
		/// <param name="employee_id"></param>
		/// <returns></returns>
		[HttpGet("{employee_id}")]
		public IEnumerable<Purchase>? GetByEmployeeId( decimal employee_id)
		{
			return _purchaseRepository.GetByEmployeeId(employee_id);
		}

		/// <summary>
		/// 采购表 通过供应商id查（若干条）
		/// </summary>
		/// <param name="supplier_id"></param>
		/// <returns></returns>
		[HttpGet("{supplier_id}")]
		public IEnumerable<Purchase>? GetBySupplierId(decimal supplier_id)
		{
			return _purchaseRepository.GetBySupplierId( supplier_id);
		}


		/// <summary>
		/// 采购表：获取所有采购信息（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Purchase> GetAll()
		{
			var purchaes = _purchaseRepository.GetPurchases();
			return purchaes;
		}

		/// <summary>
		/// 采购表 主码：PurchaseId
		/// </summary>
		/// <param name="purchase"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Purchase purchase)
		{
			return _purchaseRepository.Update(purchase);
		}

		[HttpDelete("{purchase_id}")]
		public bool Delete(decimal purchase_id)
		{
			return _purchaseRepository.Delete(purchase_id);
		}

		/// <summary>
		/// 采购表 增
		/// </summary>
		/// <param name="purchase"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Purchase purchase)
		{
			return _purchaseRepository.Add(purchase);
		}


	}
}
