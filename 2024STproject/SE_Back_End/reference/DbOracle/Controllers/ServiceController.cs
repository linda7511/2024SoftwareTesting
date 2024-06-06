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
	
	public class ServiceController : Controller
	{

		private readonly IServiceRepository _serviceRepository;

		private readonly ILogger<ServiceController> _logger;
		public ServiceController(ILogger<ServiceController> logger, IServiceRepository serviceRepository)
		{
			_logger = logger;
			_serviceRepository = serviceRepository;
		}

		[HttpGet("{customer_id}/{emp_id}/{service_time}")]
		public Service? Get(decimal customer_id, decimal emp_id, DateTime service_time)
		{
			return _serviceRepository.Get(customer_id, emp_id, service_time);
		}

		/// <summary>
		/// 服务表 主码：CustomerId、EmpId、ServiceTime
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Service service)
		{
			return _serviceRepository.Update(service);
		}

		[HttpDelete("{customer_id}/{emp_id}/{service_time}")]
		public bool Delete(decimal customer_id, decimal emp_id, DateTime service_time)
		{
			return _serviceRepository.Delete(customer_id, emp_id, service_time);
		}

		/// <summary>
		/// 服务表 增
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Service service)
		{
			return _serviceRepository.Add(service);
		}

	}
}
