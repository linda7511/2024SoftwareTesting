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
	
	public class StagingController : Controller
	{

		private readonly IStagingRepository _stagingRepository;

		private readonly ILogger<StagingController> _logger;
		public StagingController(ILogger<StagingController> logger, IStagingRepository stagingRepository)
		{
			_logger = logger;
			_stagingRepository = stagingRepository;
		}

		[HttpGet("{id}")]
		public Staging? Get(decimal id)
		{
			return _stagingRepository.Get(id);
		}

		/// <summary>
		/// 暂存表 主码：LuggageId
		/// </summary>
		/// <param name="staging"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Staging staging)
		{
			return _stagingRepository.Update(staging);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _stagingRepository.Delete(id);
		}

		/// <summary>
		/// 不要传LuggageId
		/// </summary>
		/// <param name="staging"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Staging staging)
		{
			return _stagingRepository.Add(staging);
		}

	}
}
