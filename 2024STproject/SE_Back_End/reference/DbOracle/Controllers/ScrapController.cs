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
	
	public class ScrapController : Controller
	{

		private readonly IScrapRepository _scrapRepository;

		private readonly ILogger<ScrapController> _logger;
		public ScrapController(ILogger<ScrapController> logger, IScrapRepository scrapRepository)
		{
			_logger = logger;
			_scrapRepository = scrapRepository;
		}

		[HttpGet("{emp_id}/{asset_id}")]
		public Scrap? Get(decimal emp_id, decimal asset_id)
		{
			return _scrapRepository.Get(emp_id, asset_id);
		}

		/// <summary>
		/// 报废表 主码：EmpId、AssetId
		/// </summary>
		/// <param name="scrap"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Scrap scrap)
		{
			return _scrapRepository.Update(scrap);
		}

		[HttpDelete("{emp_id}/{asset_id}")]
		public bool Delete(decimal emp_id, decimal asset_id)
		{
			return _scrapRepository.Delete(emp_id, asset_id);
		}

		/// <summary>
		/// 报废表 增 同时将对应的资产信息状态修改为报废
		/// </summary>
		/// <param name="scrap"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Scrap scrap)
		{
			return _scrapRepository.Add(scrap);
		}

	}
}
