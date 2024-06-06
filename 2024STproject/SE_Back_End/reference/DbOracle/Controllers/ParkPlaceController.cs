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
	
	public class ParkPlaceController : Controller
	{

		private readonly IParkPlaceRepository _parkPlaceRepository;

		private readonly ILogger<ParkPlaceController> _logger;
		public ParkPlaceController(ILogger<ParkPlaceController> logger, IParkPlaceRepository parkPlaceRepository)
		{
			_logger = logger;
			_parkPlaceRepository = parkPlaceRepository;
		}

		[HttpGet("{id}")]
		public ParkPlace? Get(decimal id)
		{
			return _parkPlaceRepository.Get(id);
		}

		/// <summary>
		/// 车位表：获取全部车位（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<ParkPlace>? GetAll()
		{
			return _parkPlaceRepository.GetAll();
		}

		/// <summary>
		/// 车位表 主码：ParkingSpaceId
		/// </summary>
		/// <param name="parkPlace"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(ParkPlace parkPlace)
		{
			return _parkPlaceRepository.Update(parkPlace);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _parkPlaceRepository.Delete(id);
		}

		/// <summary>
		/// 不要传ParkingSpaceId
		/// </summary>
		/// <param name="parkPlace"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(ParkPlace parkPlace)
		{
			return _parkPlaceRepository.Add(parkPlace);
		}

	}
}
