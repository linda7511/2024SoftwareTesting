using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DbOracle.Controllers;
using Microsoft.AspNetCore.Cors;
using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace DbOracle.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
	
	public class PostController
    {
        private readonly IPostRepository _postRepository;

        private readonly ILogger<PostController> _logger;
        public PostController(ILogger<PostController> logger, IPostRepository postRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
        }

        [HttpGet("{id}")]
        public Post? Get(decimal id)
        {
            return _postRepository.Get(id);
        }

        /// <summary>
        /// 岗位表：根据部门ID查（若干条）
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("{departmentId}")]
        public IEnumerable<Post>? GetByDeptID(decimal departmentId)
        {
            return _postRepository.GetByDeptID(departmentId);
        }

        /// <summary>
        /// 岗位表 获取所有岗位信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Post>? GetAll()
        {
            return _postRepository.GetAll();
        }

        /// <summary>
        /// 岗位表 主码：PostId
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Post post)
        {
            return _postRepository.Update(post);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        /// <summary>
        /// 不要传PostId
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(Post post)
        {
            return _postRepository.Add(post);
        }
    }
}
