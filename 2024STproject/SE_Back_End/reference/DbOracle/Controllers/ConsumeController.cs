using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DbOracle.Controllers;
using Microsoft.AspNetCore.Cors;
using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.AspNetCore.Authorization;
using System;

namespace DbOracle.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
	
	public class ConsumeController : Controller
    {

        private readonly IConsumeRepository _consumeRepository;

        private readonly ILogger<ConsumeController> _logger;
        public ConsumeController(ILogger<ConsumeController> logger, IConsumeRepository consumeRepository)
        {
            _logger = logger;
            _consumeRepository = consumeRepository;
        }

        [HttpGet("{DepartmentId}/{GoodsId}")]
        public Consume? Get(decimal DepartmentId, decimal GoodsId)
        {
            return _consumeRepository.Get(DepartmentId, GoodsId);
        }

        /// <summary>
        /// 消耗表 通过部门id获取信息（若干条）
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        [HttpGet("{DepartmentId}")]
        public IEnumerable<Consume>? GetByDepartmentId(decimal DepartmentId)
        {
            return _consumeRepository.GetByDepartmentId(DepartmentId);
        }

        /// <summary>
        /// 消耗表 通过物资id获取信息（若干条）
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
        [HttpGet("{GoodsId}")]
        public IEnumerable<Consume>? GetByGoodsId(decimal GoodsId)
        {
            return _consumeRepository.GetByGoodsId(GoodsId);
        }

        /// <summary>
        /// 消耗表 返回所有消耗表信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Consume>? GetAll()
        {
            return _consumeRepository.GetAll();
        }

        /// <summary>
        /// 消耗表 主码：DepartmentId、GoodsId
        /// </summary>
        /// <param name="consume"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Consume consume)
        {
            return _consumeRepository.Update(consume);
        }

        [HttpDelete("{DepartmentId}/{GoodsId}")]
        public bool Delete(decimal DepartmentId, decimal GoodsId)
        {
            return _consumeRepository.Delete(DepartmentId, GoodsId);
        }

        [HttpPost]
        public bool Add(Consume consume)
        {
            return _consumeRepository.Add(consume);
        }

    }
}
