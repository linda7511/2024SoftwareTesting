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
	
	public class LocationController : Controller
	{

		private readonly ILocationRepository _locationRepository;

		private readonly ILogger<LocationController> _logger;
		public LocationController(ILogger<LocationController> logger, ILocationRepository locationRepository)
		{
			_logger = logger;
			_locationRepository = locationRepository;
		}

		[HttpGet("{id}")]
		public Location? Get(decimal id)
		{
			return _locationRepository.Get(id);
		}

		/// <summary>
		/// 位置表 主码：LocationId
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Location location)
		{
			return _locationRepository.Update(location);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _locationRepository.Delete(id);
		}

		/// <summary>
		/// 不要传LocationId
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Location location)
		{
			return _locationRepository.Add(location);
		}

	}
}
