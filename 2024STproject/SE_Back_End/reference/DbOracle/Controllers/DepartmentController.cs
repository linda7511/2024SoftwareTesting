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
	
	public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentRepository;

        private readonly ILogger<DepartmentController> _logger;
        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
        }

        [HttpGet("{id}")]
        public Department? Get(decimal id)
        {
            return _departmentRepository.Get(id);
        }

        /// <summary>
        /// 获取所有部门信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Department>? GetAll()
        {
            return _departmentRepository.GetAll();
        }

        /// <summary>
        /// 部门表：通过部门名称获取部门信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public Department? GetByName(string name)
        {
            return _departmentRepository.GetByName(name);
        }

        /// <summary>
        /// 部门表 主码：DepartmentId
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Department department)
        {
            return _departmentRepository.Update(department);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _departmentRepository.Delete(id);
        }

        /// <summary>
        /// 不要传DepartmentId
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(Department department)
        {
            return _departmentRepository.Add(department);
        }

    }
}
