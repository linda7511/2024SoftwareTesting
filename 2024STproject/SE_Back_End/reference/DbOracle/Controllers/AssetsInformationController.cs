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
	
	public class AssetsInformationController : Controller
    {

        private readonly IAssetsInformationRepository _assetsInformationRepository;

        private readonly ILogger<AssetsInformationController> _logger;
        public AssetsInformationController(ILogger<AssetsInformationController> logger, IAssetsInformationRepository assetsInformationRepository)
        {
            _logger = logger;
            _assetsInformationRepository = assetsInformationRepository;
        }

        [HttpGet("{id}")]
        public AssetsInformation? Get(decimal id)
        {
            return _assetsInformationRepository.Get(id);
        }

        /// <summary>
        /// 资产信息表 获取所有资产信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AssetsInformation>? GetAll()
        {
            return _assetsInformationRepository.GetAll();
        }

        /// <summary>
        /// 资产信息表 传入是否报废（bool型），返回报销/未报销的资产（若干条）
        /// </summary>
        /// <param name="scraped"></param>
        /// <returns></returns>
        [HttpGet("{scraped}")]
        public IEnumerable<AssetsInformation>? GetByIfScraped(bool scraped)
        {
            return _assetsInformationRepository.GetByIfScraped(scraped);
        }

        /// <summary>
        /// 资产信息表 返回所有未报废的资产即对应位置（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Asset_Location>? GetUnscrapedAssets()
        {
            return _assetsInformationRepository.GetUnscrapedAssets();
        }

        /// <summary>
        /// 资产信息表 主码：AssetId
        /// </summary>
        /// <param name="assetsInformation"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(AssetsInformation assetsInformation)
        {
            return _assetsInformationRepository.Update(assetsInformation);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _assetsInformationRepository.Delete(id);
        }

        /// <summary>
        /// 不要传AssetId
        /// </summary>
        /// <param name="AssetsInformation"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(AssetsInformation AssetsInformation)
        {
            return _assetsInformationRepository.Add(AssetsInformation);
        }

    }
}
