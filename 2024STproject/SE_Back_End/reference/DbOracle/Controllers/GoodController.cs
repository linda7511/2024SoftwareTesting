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
	
	public class GoodController : Controller
	{

		private readonly IGoodRepository _goodRepository;

		private readonly ILogger<GoodController> _logger;
		public GoodController(ILogger<GoodController> logger, IGoodRepository goodRepository)
		{
			_logger = logger;
			_goodRepository = goodRepository;
		}

		[HttpGet("{id}")]
		public Good? Get(decimal id)
		{
			return _goodRepository.Get(id);
		}

		/// <summary>
		/// 物资表 通过员工id查（对应员工采购的某些类物资）（若干条）
		/// </summary>
		/// <param name="EmpId"></param>
		/// <returns></returns>
        [HttpGet("{EmpId}")]
        public IEnumerable<Emp_Good>? GetByEmpId(decimal EmpId)
        {
            return _goodRepository.GetByEmpId(EmpId);
        }

        /// <summary>
        /// 物资表 返回所有物资信息（若干条）
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Good>? GetAllInfo()
        {
            return _goodRepository.GetAllInfo();
        }

		/// <summary>
		/// 物资表 通过物资名查询
		/// </summary>
		/// <param name="goodsName"></param>
		/// <returns></returns>
		[HttpGet("{goodsName}")]
        public Good? GetByName(string goodsName)
		{
			return _goodRepository.GetByName(goodsName);
		}

		/// <summary>
		/// 物资表 主码：GoodsId
		/// </summary>
		/// <param name="good"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Good good)
		{
			return _goodRepository.Update(good);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _goodRepository.Delete(id);
		}

		/// <summary>
		/// 不要传GoodsId
		/// </summary>
		/// <param name="good"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Good good)
		{
			return _goodRepository.Add(good);
		}
	}
}
