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
	
	public class SupplierController
	{
		private readonly ISupplierRepository _supplierRepository;

		private readonly ILogger<SupplierController> _logger;
		public SupplierController(ILogger<SupplierController> logger, ISupplierRepository supplierRepository)
		{
			_logger = logger;
			_supplierRepository = supplierRepository;
		}

		[HttpGet("{id}")]
		public Supplier? Get(decimal id)
		{
			return _supplierRepository.Get(id);
		}

		/// <summary>
		/// 供应商表 通过物资号查（若干条）
		/// </summary>
		/// <param name="goodsId"></param>
		/// <returns></returns>
        [HttpGet("{goodsId}")]
        public IEnumerable<Supplier>? GetByGoodsId(decimal goodsId)
		{
			return _supplierRepository.GetByGoodsId(goodsId);
        }

		/// <summary>
		/// 供应商表 主码：SupplierId
		/// </summary>
		/// <param name="supplier"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Supplier supplier)
		{
			return _supplierRepository.Update(supplier);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _supplierRepository.Delete(id);
		}

		/// <summary>
		/// 不要传SupplierId
		/// </summary>
		/// <param name="supplier"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Supplier supplier)
		{
			return _supplierRepository.Add(supplier);
		}

	}
}
