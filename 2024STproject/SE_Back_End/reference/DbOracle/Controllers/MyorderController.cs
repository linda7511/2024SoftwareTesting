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
	
	public class MyorderController : Controller
	{

		private readonly IMyorderRepository _myOrderRepository;

		private readonly ILogger<MyorderController> _logger;
		public MyorderController(ILogger<MyorderController> logger, IMyorderRepository myOrderRepository)
		{
			_logger = logger;
			_myOrderRepository = myOrderRepository;
		}

        /// <summary>
        /// 点单表、菜品表联合查 通过桌位id查对应的点单和菜品信息（若干条）
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [HttpGet("{tableId}")]
        public IEnumerable<OrderAndDish>? GetOrderAndDishInfo(decimal tableId)
        {
            return _myOrderRepository.GetOrderAndDishInfo(tableId);
        }

        [HttpGet("{table_id}/{dish_id}/{order_time}")]
		public Myorder? Get(decimal table_id, decimal dish_id, DateTime order_time)
		{
			return _myOrderRepository.Get(table_id, dish_id, order_time);
		}

		/// <summary>
		/// 点单表 主码：TableId、DishId、OrderTime
		/// </summary>
		/// <param name="myOrder"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Myorder myOrder)
		{
			return _myOrderRepository.Update(myOrder);
		}

		[HttpDelete("{table_id}/{dish_id}/{order_time}")]
		public bool Delete(decimal table_id, decimal dish_id, DateTime order_time)
		{
			return _myOrderRepository.Delete(table_id, dish_id, order_time);
		}

		/// <summary>
		/// 点单表 增
		/// </summary>
		/// <param name="myOrder"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Myorder myOrder)
		{
			return _myOrderRepository.Add(myOrder);
		}

	}
}
