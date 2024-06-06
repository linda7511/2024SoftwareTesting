using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DbOracle.Controllers;
using Microsoft.AspNetCore.Cors;
using DbOracle.Models;
using DbOracle.Repository;
using DbOracle.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DbOracle.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	[EnableCors("any")]
	public class EmployeeController : Controller
	{

		private readonly IEmployeeRepository _employeeRepository;

		private readonly ILogger<EmployeeController> _logger;

		private readonly TokenManager _jwtService;
		public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository, TokenManager jwtService)
		{
			_logger = logger;
			_employeeRepository = employeeRepository;
			_jwtService = jwtService;
		}

		/// <summary>
		/// 登录 输入账号密码返回状态信息
		/// </summary>
		/// <param name="account"></param>
		/// <param name="password"></param>
		[AllowAnonymous]
		[HttpGet("{account}/{password}")]
		public LoginResult Login(string account, string password)
		{
			string my_password = Encrypter.encrypt(password);
			LoginResult result = _employeeRepository.Login(account, my_password);
			if (result.StatusCode == 0)
			{
				string id = result.employeeInfo.EmployeeId.ToString();
				string name = result.employeeInfo.Account;
				result.employeeInfo.Password = "";
				result.token = _jwtService.CreateToken(id, name);
			}
			return result;
		}

		[HttpGet("{id}")]
		
		public Employee? Get(decimal id)
		{
			return _employeeRepository.Get(id);
		}

		/// <summary>
		/// 员工表：通过员工ID查员工信息
		/// </summary>
		/// <param name="employeeID"></param>
		/// <returns></returns>
		[HttpGet("{employeeID}")]
		
		public Employee? GetByEmpId(decimal employeeID)
		{
			return _employeeRepository.GetByEmpId(employeeID);
		}

		/// <summary>
		/// 员工表：通过账号查
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		[HttpGet]
		public Employee? GetByAccount(string account)
        {
			return _employeeRepository.GetByAccount(account);
		}

		/// <summary>
		/// 员工表：根据员工姓名查员工信息（考虑重名情况，可能返回若干条）
		/// </summary>
		/// <param name="employeeName"></param>
		/// <returns></returns>
		[HttpGet("{employeeName}")]
		public IEnumerable<Employee>? GetByName(string employeeName)
		{
			return _employeeRepository.GetByName(employeeName);
		}


		/// <summary>
		/// 员工表 岗位表 部门表 ：联合查表， 查看所有员工基本信息
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
		[HttpGet]
		
		public IEnumerable<CombinedDataEmployeeMessage>? GetAll()
		{
			return _employeeRepository.GetAll();
		}

		/// <summary>
		/// 员工表 主码：EmployeeId，更新成功返回新token，失败返回failed
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
		[HttpPut]
		
		public string Update(Employee employee)
		{
			/*
			if (_employeeRepository.GetByAccount(employee.Account) == null)
			{
				return "Account is not exist!";
			}
			*/
			if (employee.Password != null)
				employee.Password = Encrypter.encrypt(employee.Password);
			Employee added_user = _employeeRepository.Update(employee);
			if (added_user != null)
			{
				string id = added_user.EmployeeId.ToString();
				string name = added_user.Account;
				return _jwtService.CreateToken(id, name);
			}
			else
			{
				return "Failed!";
			}
		}


		[HttpDelete("{id}")]
		
		public bool Delete(decimal id)
		{
			return _employeeRepository.Delete(id);
		}

		/// <summary>
		/// 员工表 增 成功则返回一个json对象，包括若干信息及token，不要传EmployeeId
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Add([FromBody] Employee employee)
		{
			if (_employeeRepository.GetByAccount(employee.Account) != null)
			{
				var response = new
				{
					status = "error",
					Message = "Account is exist!"
				};
				return Json(response);
				//return false;
			}
			employee.Password = Encrypter.encrypt(employee.Password);
			Employee added_user = _employeeRepository.Add(employee);
			if (added_user != null)
			{
				string id = added_user.EmployeeId.ToString();
				string name = added_user.Account;
				//var token=  _jwtService.CreateToken(id, name);
				var response = new
				{
					status = "success",
					token = _jwtService.CreateToken(id, name)
				};
				return Json(response);
			}
			else
			{
				var response = new
				{
					status = "error",
					message = "add error"
				};
				return Json(response);
				//return false;
			}
		}
	}
}
	