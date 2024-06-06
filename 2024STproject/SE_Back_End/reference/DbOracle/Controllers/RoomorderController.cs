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
	
	public class RoomorderController : Controller
	{

		private readonly IRoomorderRepository _roomOrderRepository;

		private readonly ILogger<RoomorderController> _logger;
		public RoomorderController(ILogger<RoomorderController> logger, IRoomorderRepository roomOrderRepository)
		{
			_logger = logger;
			_roomOrderRepository = roomOrderRepository;
		}

		[HttpGet("{id}")]
		public Roomorder? Get(decimal id)
		{
			return _roomOrderRepository.Get(id);
		}

		/// <summary>
		/// 订单表：获取所有订单（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Roomorder>? GetAll()
		{
			return _roomOrderRepository.GetAll();
		}

		/// <summary>
		/// 订单表：通过顾客id获取订单（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet("{CustomerId}")]
		public IEnumerable<Roomorder>? GetByCustomerId(decimal CustomerId)
		{
			return _roomOrderRepository.GetByCustomerId(CustomerId);
		}

		/// <summary>
		/// 订单表 主码：OrderId
		/// </summary>
		/// <param name="roomOrder"></param>
		/// <returns></returns>
		[HttpPut]
        public bool Update(Roomorder roomOrder)
        {
			return _roomOrderRepository.Update(roomOrder);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _roomOrderRepository.Delete(id);
		}

		/// <summary>
		/// 不要传入 OrderId
		/// </summary>
		[HttpPost]
		public bool Add(Roomorder roomOrder)
		{
			return _roomOrderRepository.Add(roomOrder);
		}

	}
}
