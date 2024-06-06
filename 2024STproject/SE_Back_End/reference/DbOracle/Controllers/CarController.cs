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
	
	public class CarController : Controller
    {

        private readonly ICarRepository _carRepository;

        private readonly ILogger<CarController> _logger;
        public CarController(ILogger<CarController> logger, ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        /// <summary>
        /// 车辆表 获取全部车辆信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Car>? GetAll()
        {
            return _carRepository.GetAll();
        }

        /// <summary>
        /// 车辆表 通过车牌号获取车辆信息
        /// </summary>
        /// <param name="carNumber"></param>
        /// <returns></returns>
        [HttpGet("{carNumber}")]
        public Car? GetByCarNumber(string carNumber)
        {
            return _carRepository.GetByCarNumber(carNumber);
        }

        /// <summary>
        /// 车辆表 主码：CarNumber
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Car car)
        {
            return _carRepository.Update(car);
        }


        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _carRepository.Delete(id);
        }


        /// <summary>
        /// 车辆表 增
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(Car car)
        {
            return _carRepository.Add(car);
        }
    }
}
