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
	
	public class CustomerController : Controller
    {

        private readonly ICustomerRepository _customerRepository;

        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }


		[HttpGet("{id}")]
        public Customer? Get(decimal id)
        {
            return _customerRepository.Get(id);
        }

        /// <summary>
        /// 客人表：获取所有顾客信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable< Customer>? GetAll()
        {
            return _customerRepository.GetAll();
        }
        /// <summary>
		/// 客人表 通过身份证号ID查
		/// </summary>
		[HttpGet("{id}")]
        public Customer? GetById(string id)
        {
            return _customerRepository.GetById(id);
        }

        /// <summary>
        /// 客人表 通过姓名查（若干条）
        /// </summary>
        [HttpGet("{name}")]
        public IEnumerable<Customer>? GetByName(string name)
        {
            return _customerRepository.GetByName(name);
        }

        /// <summary>
        /// 客人表 通过电话查（若干条）
        /// </summary>
        [HttpGet("{phone}")]
        public IEnumerable<Customer>? GetByPhone(string phone)
        {
            return _customerRepository.GetByPhone(phone);
        }

        /// <summary>
        /// 客人表 主码：CustomerId
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Customer customer)
        {
            return _customerRepository.Update(customer);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _customerRepository.Delete(id);
        }

        /// <summary>
        /// 不要传CustomerId
        /// </summary>
        [HttpPost]
        public bool Add(Customer customer)
        {
            return _customerRepository.Add(customer);
        }

        /// <summary>
        /// 客人表+入住表+房间表 联合多表查 通过房间号查客人信息（若干条）
        /// </summary>
        [HttpGet("{roomNum}")]
        public IEnumerable<CusAndTime> GetCustomerAndTimeInfo(decimal roomNum)
        {
            return _customerRepository.GetCustomerAndTimeInfo(roomNum);
        }
    }
}
