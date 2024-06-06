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
	
	public class DishController : Controller
	{
		
		private readonly IDishRepository _dishRepository;

		private readonly ILogger<DishController> _logger;
		public DishController(ILogger<DishController> logger, IDishRepository dishRepository)
		{
			_logger = logger;
            _dishRepository = dishRepository;
		}

		[HttpGet("{id}")]
		public Dish? Get(decimal id)
		{
			return _dishRepository.Get(id);
		}

        /// <summary>
        /// 菜单表 返回所有菜单信息（若干条）
        /// </summary>
        [HttpGet]
        public IEnumerable<Dish> GetAllDishes()
        {
            return _dishRepository.GetAllDishes();
        }

        /// <summary>
		/// 菜单表 返回每一桌的所有菜品信息（若干条）
		/// </summary>
		/// <param name="tableId"></param>
		/// <returns></returns>
        [HttpGet]
        public IEnumerable<Table_Dish>? GetEachTableDishes()
        {
            return _dishRepository.GetEachTableDishes();
        }

		/// <summary>
		/// 菜单表 主码：DishId
		/// </summary>
		/// <param name="dish"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Dish dish)
		{
			return _dishRepository.Update(dish);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _dishRepository.Delete(id);
		}

		/// <summary>
		/// 不要传入 DishId
		/// </summary>
		[HttpPost]
		public bool Add(Dish dish)
		{
			return _dishRepository.Add(dish);
		}
    }
}
