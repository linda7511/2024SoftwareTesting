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
	
	public class ParkingController : Controller
    {

        private readonly IParkingRepository _parkingRepository;

        private readonly ILogger<ParkingController> _logger;
        public ParkingController(ILogger<ParkingController> logger, IParkingRepository parkingRepository)
        {
            _logger = logger;
            _parkingRepository = parkingRepository;
        }

        [HttpGet("{parking_space_id}/{car_number}/{start_time}")]
        public Parking? Get(decimal parking_space_id, string car_number, DateTime start_time)
        {
            return _parkingRepository.Get(parking_space_id, car_number, start_time);
        }

        /// <summary>
        /// 停放表 车位表 ：联合查（若干条）
        /// </summary>
        [HttpGet]
        public IEnumerable<CombinedDataOccupancy> GetByParkingAndPlace()
        {
            return _parkingRepository.GetByParkingAndPlace();
        }

        /// <summary>
        /// 停放表 车位表 车位单价表：联合查（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CombinedDataParking> GetAll()
        {
            return _parkingRepository.GetAll();
        }

        /// <summary>
        /// 停放表 车位表 车位单价表：按车牌号联合查（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{car_number}")]
        public IEnumerable<CombinedDataParking> GetByCarNumber(string car_number)
        {
            return _parkingRepository.GetByCarNumber(car_number);
        }

        /// <summary>
        /// 停放表 车位表 车位单价表：按停放开始结束时间范围联合查（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{earliest}/{latest}")]
        public IEnumerable<CombinedDataParking> GetByTime(DateTime earliest, DateTime latest)
        {
            return _parkingRepository.GetByTime(earliest, latest);
        }

        /// <summary>
        /// 停放表 车位表 车位单价表：按车位类型联合查（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{type}")]
        public IEnumerable<CombinedDataParking> GetByParkingPlaceType(string type)
        {
            return _parkingRepository.GetByParkingPlaceType(type);
        }

        /// <summary>
        /// 停放表 车位表 车位单价表：按片区联合查（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{area}")]
        public IEnumerable<CombinedDataParking> GetByArea(string area)
        {
            return _parkingRepository.GetByArea(area);
        }

        /// <summary>
        /// 停放表 车位表 车位单价表：按占用状态联合查（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{status}")]
        public IEnumerable<CombinedDataParking> GetByStatus(string status)
        {
            return _parkingRepository.GetByStatus(status);
        }

        /// <summary>
        /// 停放表 车位表 车位单价表：按价格范围联合查（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet("{min_price}/{max_price}")]
        public IEnumerable<CombinedDataParking> GetByPrice(decimal min_price, decimal max_price)
        {
            return _parkingRepository.GetByPrice(min_price, max_price);
        }


        /// <summary>
        /// 停放表 主码：ParkingSpaceId、CarNumber、StartTime
        /// </summary>
        /// <param name="parking"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Parking parking)
        {
            return _parkingRepository.Update(parking);
        }


        [HttpDelete("{parking_space_id}/{car_number}/{start_time}")]
        public bool Delete(decimal parking_space_id, string car_number, DateTime start_time)
        {
            return _parkingRepository.Delete(parking_space_id, car_number, start_time);
        }

        /// <summary>
        /// 停放表 增
        /// </summary>
        /// <param name="parking"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(Parking parking)
        {
            return _parkingRepository.Add(parking);
        }

    }
}
