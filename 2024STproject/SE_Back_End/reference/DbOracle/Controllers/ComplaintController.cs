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
	
	public class ComplaintController : Controller
    {

        private readonly IComplaintRepository _complaintRepository;

        private readonly ILogger<ComplaintController> _logger;
        public ComplaintController(ILogger<ComplaintController> logger, IComplaintRepository complaintRepository)
        {
            _logger = logger;
            _complaintRepository = complaintRepository;
        }

        [HttpGet("{id}")]
        public Complaint? Get(decimal id)
        {
            return _complaintRepository.Get(id);
        }

        /// <summary>
        /// 投诉表 主码 ComplaintId
        /// </summary>
        /// <param name="complaint"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Complaint complaint)
        {
            return _complaintRepository.Update(complaint);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _complaintRepository.Delete(id);
        }

        /// <summary>
        /// 不要传ComplaintId
        /// </summary>
        /// <param name="complaint"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(Complaint complaint)
        {
            return _complaintRepository.Add(complaint);
        }

    }
}
