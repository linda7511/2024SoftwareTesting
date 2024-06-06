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
	
	public class RoomTypeController
	{
		private readonly IRoomtypeRepository _roomTypeRepository;

		private readonly ILogger<RoomTypeController> _logger;
		public RoomTypeController(ILogger<RoomTypeController> logger, IRoomtypeRepository roomTypeRepository)
		{
			_logger = logger;
			_roomTypeRepository = roomTypeRepository;
		}

		[HttpGet("{id}")]
		public Roomtype? Get(decimal id)
		{
			return _roomTypeRepository.Get(id);
		}

        /// <summary>
        /// 房间类型表 通过价格范围查（若干条）
        /// </summary>
        [HttpGet("{price_up}/{price_down}")]
        public IEnumerable<Roomtype> GetBetween(decimal price_up, decimal price_down)
        {
            if (price_up == null || price_down == null)
            {
                return null;
            }
            else
            {
                return _roomTypeRepository.GetBetween(price_up, price_down);
            }
        }

        /// <summary>
        /// 房间类型表 通过类型名称查
        /// </summary>
        [HttpGet("{typeName}")]
        public IEnumerable<CombinedDataRoomtype> GetByType(string typeName)
        {
            return _roomTypeRepository.GetByType(typeName);
        }

		/// <summary>
		/// 房间类型表 主码：TypeId
		/// </summary>
		/// <param name="roomType"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Roomtype roomType)
		{
			return _roomTypeRepository.Update(roomType);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _roomTypeRepository.Delete(id);
		}

		/// <summary>
		/// 不要传入 TypeId
		/// </summary>
		[HttpPost]
		public bool Add(Roomtype roomType)
		{
			return _roomTypeRepository.Add(roomType);
		}
    }
}
