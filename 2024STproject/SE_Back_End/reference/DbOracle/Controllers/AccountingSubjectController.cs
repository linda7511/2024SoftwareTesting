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
	
	public class AccountingSubjectController : Controller
    {

        private readonly IAccountingSubjectRepository _accountingSubjectRepository;

        private readonly ILogger<AccountingSubjectController> _logger;
        public AccountingSubjectController(ILogger<AccountingSubjectController> logger, IAccountingSubjectRepository AccountingSubjectRepository)
        {
            _logger = logger;
            _accountingSubjectRepository = AccountingSubjectRepository;
        }


        [HttpGet("{id}")]
        public AccountingSubject? Get(decimal id)
        {
            return _accountingSubjectRepository.Get(id);
        }

        /// <summary>
        /// 会计科目表主码：AccountSubjectId
        /// </summary>
        /// <param name="accountingSubject"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(AccountingSubject accountingSubject)
        {
            return _accountingSubjectRepository.Update(accountingSubject);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _accountingSubjectRepository.Delete(id);
        }

        /// <summary>
        /// 不要传AccountSubjectId
        /// </summary>
        /// <param name="accountingSubject"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(AccountingSubject accountingSubject)
        {
            return _accountingSubjectRepository.Add(accountingSubject);
        }

    }
}
