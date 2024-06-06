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
	
	public class SalaryController : Controller
	{

		private readonly ISalaryRepository _salaryRepository;

		private readonly ILogger<SalaryController> _logger;
		public SalaryController(ILogger<SalaryController> logger, ISalaryRepository salaryRepository)
		{
			_logger = logger;
			_salaryRepository = salaryRepository;
		}

		[HttpGet("{id}")]
		public Salary? Get(decimal id)
		{
			return _salaryRepository.Get(id);
		}

        /// <summary>
        /// 工资表 根据员工ID查工资信息（若干条）
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpGet("{employeeID}")]
        public IEnumerable<Salary>? GetByEmployeeID(decimal employeeID)
        {
            return _salaryRepository.GetByEmployeeID(employeeID);
        }

		/// <summary>
		/// 工资表 主码：SalaryId
		/// </summary>
		/// <param name="salary"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Salary salary)
		{
			return _salaryRepository.Update(salary);
		}

		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _salaryRepository.Delete(id);
		}

		/// <summary>
		/// 不要传SalaryId
		/// </summary>
		/// <param name="salary"></param>
		/// <returns></returns>
		[HttpPost]
		public bool Add(Salary salary)
		{
			return _salaryRepository.Add(salary);
		}

	}
}
