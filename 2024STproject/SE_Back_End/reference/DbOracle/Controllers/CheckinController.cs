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
	
	public class CheckinController : Controller
	{
		
		private readonly ICheckinRepository _checkinRepository;

		private readonly ILogger<CheckinController> _logger;
		public CheckinController(ILogger<CheckinController> logger, ICheckinRepository checkinRepository)
		{
			_logger = logger;
			_checkinRepository = checkinRepository;
		}

		[HttpGet("{CustomerId}/{RoomId}/{InTime}")]
		public Checkin? Get(decimal CustomerId, decimal RoomId, DateTime InTime)
		{
			return _checkinRepository.Get(CustomerId, RoomId, InTime);
		}

		/// <summary>
		/// 入住表 获取所有入住信息（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Checkin>? GetAll()
		{
			return _checkinRepository.GetAll();
		}

		/// <summary>
		/// 入住表 通过顾客id和入住时间查
		/// </summary>
		/// <param name="customerId"></param>
		/// <param name="inTime"></param>
		/// <returns></returns>
		[HttpGet("{CustomerId}/{InTime}")]
		public IEnumerable<Checkin>? GetByCustomerIdAndInTime(decimal customerId,DateTime inTime)
		{
			return _checkinRepository.GetByCustomerIdAndInTime(customerId, inTime);
		}

		/// <summary>
		/// 入住表 通过房间号查（若干条）
		/// </summary>
		[HttpGet("{roomNum}")]
		public IEnumerable<Checkin>? GetByRoomNum(decimal roomNum)
		{
			return _checkinRepository.GetByRoomNum(roomNum);
		}

		/// <summary>
		/// 入住表 主码：CustomerId、RoomId、InTime
		/// </summary>
		/// <param name="checkin"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Checkin checkin)
		{
			return _checkinRepository.Update(checkin);
		}

		[HttpDelete("{CustomerId}/{RoomId}/{InTime}")]
		public bool Delete(decimal CustomerId, decimal RoomId, DateTime InTime)
		{
			return _checkinRepository.Delete(CustomerId, RoomId, InTime);
		}



		/// <summary>
		/// 不要传CheckInId
		/// </summary>
		[HttpPost]
		public bool Add(Checkin checkin)
		{
			return _checkinRepository.Add(checkin);
		}

	}
}
