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
	
	public class MytableController : Controller
	{

		private readonly IMytableRepository _myTableRepository;

		private readonly ILogger<MytableController> _logger;
		public MytableController(ILogger<MytableController> logger, IMytableRepository myTableRepository)
		{
			_logger = logger;
			_myTableRepository = myTableRepository;
		}


        [HttpGet("{id}")]
		public Mytable? Get(decimal id)
		{
			return _myTableRepository.Get(id);
		}

		/// <summary>
		/// 桌位表 查询所有空闲桌位的信息（若干条）
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        public IEnumerable<Mytable> GetLeisureTable()
        {
            return _myTableRepository.GetLeisureTable();
        }

		/// <summary>
		/// 桌位表 返回所有桌位信息（若干条）
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        public IEnumerable<Mytable>? GetAll()
		{

			return _myTableRepository.GetAll();
		}

		/// <summary>
		/// 桌位表 主码：TableId
		/// </summary>
		/// <param name="newTable"></param>
		/// <returns></returns>
		[HttpPut]
        public bool Update(Mytable newTable)
		{
			return _myTableRepository.Update(newTable);
		}

        /// <summary>
        /// 桌位表 通过桌位id修改状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        [HttpPut("{tableId}/{newStatus}")]
        public bool? UpdateStatus(decimal tableId, string newStatus)
        {
            return _myTableRepository.UpdateStatus(tableId, newStatus);
        }

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _myTableRepository.Delete(id);
		}

		/// <summary>
		/// 不要传TableId
		/// </summary>
		/// <param name="myTable"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Mytable myTable)
		{
			return _myTableRepository.Add(myTable);
		}

	}
}
