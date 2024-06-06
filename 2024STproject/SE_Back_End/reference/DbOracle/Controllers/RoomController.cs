using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DbOracle.Controllers;
using Microsoft.AspNetCore.Cors;
using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.AspNetCore.Authorization;
using System;

namespace DbOracle.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	[EnableCors("any")]

	public class RoomController : Controller
	{

		private readonly IRoomRepository _roomRepository;

		private readonly ILogger<RoomController> _logger;
		public RoomController(ILogger<RoomController> logger, IRoomRepository roomRepository)
		{
			_logger = logger;
			_roomRepository = roomRepository;
		}

		[HttpGet("{id}")]
		public Room? Get(decimal id)
		{
			return _roomRepository.Get(id);
		}

		/// <summary>
		/// 房间表：通过房间类型查空房间（若干条）
		/// </summary>
		/// <param name="typeId"></param>
		/// <returns></returns>
		[HttpGet("{typeId}")]
		public IEnumerable<Room>? GetEmptyRoomsByType(decimal typeId)
		{
			return _roomRepository.GetEmptyRoomsByType(typeId);
		}

		/// <summary>
		/// 房间表：查所有空房间（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Room>? GetEmptyRoomsAll()
		{
			return _roomRepository.GetEmptyRoomsAll();
		}

		/// <summary>
		/// 房间表：通过房间号获取房间id
		/// </summary>
		/// <param name="roomNum"></param>
		/// <returns></returns>
		[HttpGet("{roomNum}")]
		public decimal ? GetRoomIdByRoomNum(decimal roomNum)
		{
			return _roomRepository.GetRoomIdByRoomNum(roomNum);
		}

		/// <summary>
		/// 房间表 通过ID查空房数量
		/// </summary>
		[HttpGet("{typeId}")]
		public int GetEmptyRoomCount(decimal typeId)
		{
			return _roomRepository.GetEmptyRoomCount(typeId);
		}

		/// <summary>
		/// 房间表 查 传入身份证号/电话/姓名 返回所有匹配的房间信息（若干条）
		/// </summary>
		/// <param name="customerInfo"></param>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<Customer_Room>? GetMatchRooms(Options customerInfo)
		{
			return _roomRepository.GetMatchRooms(customerInfo);
		}

		/// <summary>
		/// 房间表 主码：RoomId
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Room room)
		{
			return _roomRepository.Update(room);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _roomRepository.Delete(id);
		}



		

		/// <summary>
		/// 不要传入 RoomId （状态取值为 已入住 / 空闲 / 维修中 三选一）
		/// </summary>
		[HttpPost]
		public bool Add(Room room)
		{
			return _roomRepository.Add(room);
		}
	}
}
