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
	
	public class AssetsSummarizeController : Controller
    {

        private readonly IAssetsSummarizeRepository _assetsSummarizeRepository;

        private readonly ILogger<AssetsSummarizeController> _logger;
        public AssetsSummarizeController(ILogger<AssetsSummarizeController> logger, IAssetsSummarizeRepository assetsSummarizeRepository)
        {
            _logger = logger;
            _assetsSummarizeRepository = assetsSummarizeRepository;
        }

        [HttpGet("{id}")]
        public AssetsSummarize? Get(decimal id)
        {
            return _assetsSummarizeRepository.Get(id);
        }

       

        /// <summary>
        /// 资产汇总表 主码：AssetsSummarizeId
        /// </summary>
        /// <param name="assetsSummarize"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(AssetsSummarize assetsSummarize)
        {
            return _assetsSummarizeRepository.Update(assetsSummarize);
        }

        [HttpDelete("{id}")]
        public bool Delete(decimal id)
        {
            return _assetsSummarizeRepository.Delete(id);
        }

        /// <summary>
        /// 不要传AssetsSummarizeId
        /// </summary>
        /// <param name="assetsSummarize"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(AssetsSummarize assetsSummarize)
        {
            return _assetsSummarizeRepository.Add(assetsSummarize);
        }

    }
}
