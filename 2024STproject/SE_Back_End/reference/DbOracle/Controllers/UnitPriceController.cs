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
	
	public class UnitPriceController : Controller
	{

		private readonly IUnitPriceRepository _unitPriceRepository;

		private readonly ILogger<UnitPriceController> _logger;
		public UnitPriceController(ILogger<UnitPriceController> logger, IUnitPriceRepository unitPriceRepository)
		{
			_logger = logger;
			_unitPriceRepository = unitPriceRepository;
		}

		[HttpGet("{parking_place_type}")]
		public UnitPrice? Get(string parking_place_type)
		{
			return _unitPriceRepository.Get(parking_place_type);
		}

        /// <summary>
        /// 车位单价表 车位表：联合查表，获取每个车位的车位信息和价格信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CombinedDataParkingPrice>? GetAll()
        {
            return _unitPriceRepository.GetAll();
        }

		/// <summary>
		/// 车位单价表 主码：ParkingPlaceType
		/// </summary>
		/// <param name="unitPrice"></param>
		/// <returns></returns>
		[HttpPut]
        public bool Update(UnitPrice unitPrice)
        {
            return _unitPriceRepository.Update(unitPrice);
        }

        [HttpDelete("{parking_place_type}")]
		public bool Delete(string parking_place_type)
		{
			return _unitPriceRepository.Delete(parking_place_type);
		}

		/// <summary>
		/// 车位单价表 增
		/// </summary>
		/// <param name="unitPrice"></param>
		/// <returns></returns>
        [HttpPost]
        public bool Add(UnitPrice unitPrice)
        {
            return _unitPriceRepository.Add(unitPrice);
        }
    }
}
