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
	
	public class ApplicationController : Controller
    {

        private readonly IApplicationRepository _applicationRepository;

        private readonly ILogger<ApplicationController> _logger;
        public ApplicationController(ILogger<ApplicationController> logger, IApplicationRepository applicationRepository)
        {
            _logger = logger;
            _applicationRepository = applicationRepository;
        }

        [HttpGet("{id}")]
        public Application? Get(decimal id)
        {
            return _applicationRepository.Get(id);
        }

        /// <summary>
        /// 申请表：获取所有申请信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Application>? GetAll()
        {
            return _applicationRepository.GetAll();
        }

        /// <summary>
        /// 申请表：根据员工ID查，返回多条申请信息（若干条）
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpGet("{employeeID}")]
        public IEnumerable<Application>? GetByEmpId(decimal employeeID)
        {
            return _applicationRepository.GetByEmpId(employeeID);
        }

        /// <summary>
        /// 申请表：根据申请类型查，返回多条申请信息（若干条）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("{type}")]
        public IEnumerable<Application>? GetByType(string type)
        {
            return _applicationRepository.GetByType(type);
        }

        /// <summary>
        /// 申请表：根据申请时间查，返回多条申请信息（精确到天，若干条）
        /// </summary>
        /// <param name="applicationTime"></param>
        /// <returns></returns>
        [HttpGet("{applicationTime}")]
        public IEnumerable<Application>? GetByApplicationTimeOfDay(DateTime applicationTime)
        {
            return _applicationRepository.GetByApplicationTimeOfDay(applicationTime);
        }

        /// <summary>
        /// 申请表主码：ApplicationId
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Application application)
        {
            return _applicationRepository.Update(application);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _applicationRepository.Delete(id);
        }

        /// <summary>
        /// 不要传ApplicationId
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        [HttpPost]
        public Application Add(Application application)
        {
            return _applicationRepository.Add(application);
        }
    }
}
