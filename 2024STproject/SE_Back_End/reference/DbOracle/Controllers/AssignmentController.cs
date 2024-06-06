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
	
	public class AssignmentController : Controller
	{
		
		private readonly IAssignmentRepository _assignmentRepository;

		private readonly ILogger<AssignmentController> _logger;
		public AssignmentController(ILogger<AssignmentController> logger, IAssignmentRepository assignmentRepository)
		{
			_logger = logger;
			_assignmentRepository = assignmentRepository;
		}


		[HttpGet("{id}")]
		public Assignment? Get(decimal id)
		{
			return _assignmentRepository.Get(id);
		}

		/// <summary>
		/// 任务表：根据部门id获取该部门下的所有任务信息(若干条)
		/// </summary>
		/// <param name="departmentId"></param>
		/// <returns></returns>
		[HttpGet("{departmentId}")]
		public  IEnumerable< Assignment>? GetByDepartmentId(decimal departmentId)
		{
			return _assignmentRepository.GetByDepartmentId(departmentId);
		}

		/// <summary>
		/// 任务表：通过任务名称查（若干条）
		/// </summary>
		/// <param name="assignmentName"></param>
		/// <returns></returns>
		[HttpGet("{assignmentName}")]
		public IEnumerable<Assignment>? GetByAssignmentName(string assignmentName)
		{
			return _assignmentRepository.GetByAssignmentName(assignmentName);
		}
		


		/// <summary>
		/// 任务表：查所有任务信息（若干条）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable< Assignment>? GetAll()
		{
			return _assignmentRepository.GetAll();
		}

		/// <summary>
		/// 任务表 主码：AssignmentId
		/// </summary>
		/// <param name="assignment"></param>
		/// <returns></returns>
		[HttpPut]
		public bool Update(Assignment assignment)
		{
			return _assignmentRepository.Update(assignment);
		}

		/// <summary>
		/// 任务表：根据任务ID删
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public bool Delete(decimal id)
		{
			return _assignmentRepository.Delete(id);
		}

		/// <summary>
		/// 不要传AssignmentId
		/// </summary>
		/// <param name="assignment"></param>
		/// <returns></returns>
		[HttpPost]
		public Assignment Add(Assignment assignment)
		{
			return _assignmentRepository.Add(assignment);
		}
	}
}
