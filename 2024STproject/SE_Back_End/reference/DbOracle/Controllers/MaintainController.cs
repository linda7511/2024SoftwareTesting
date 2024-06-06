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
	
	public class MaintainController : Controller
	{

		private readonly IMaintainRepository _maintainRepository;

		private readonly ILogger<MaintainController> _logger;
		public MaintainController(ILogger<MaintainController> logger, IMaintainRepository maintainRepository)
		{
			_logger = logger;
			_maintainRepository = maintainRepository;
		}

		[HttpGet("{room_id}/{emp_id}/{maintain_time}")]
		public Maintain? Get(decimal room_id, decimal emp_id, DateTime maintain_time)
		{
			return _maintainRepository.Get(room_id, emp_id,maintain_time);
		}

		/// <summary>
		/// 维修表 返回所有维修信息（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public IEnumerable<Maintain>? GetAll()
		{
			return _maintainRepository.GetAll();
		}

		/// <summary>
		/// 维修表 主码：RoomId、EmpId、MaintainTime
		/// </summary>
		/// <param name="maintain"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Maintain maintain)
		{
			return _maintainRepository.Update(maintain);
		}

		/// <summary>
		/// 维修表 通过roomId查（若干条）
		/// </summary>
		/// <param name="roomId"></param>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Maintain> GetByRoom(decimal roomId)
		{
			return _maintainRepository.GetByRoom(roomId);
		}

		/// <summary>
		/// 维修表 通过物品名查（若干条）
		/// </summary>
		/// <param name="objectName"></param>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Maintain> GetByObject(string objectName)
		{
			return _maintainRepository.GetByObject(objectName);
		}

		[HttpDelete("{room_id}/{emp_id}/{maintain_time}")]
		public bool Delete(decimal room_id, decimal emp_id, DateTime maintain_time)
		{
			return _maintainRepository.Delete(room_id, emp_id, maintain_time);
		}

		/// <summary>
		/// 维修表 增
		/// </summary>
		/// <param name="maintain"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Room_maintain maintain)
		{
			return _maintainRepository.Add(maintain);
		}

	}
}
