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
	
	public class BookController : Controller
    {

        private readonly IBookRepository _bookRepository;

        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// 预定表 通过TableId和CustomerId查餐饮预定信息（若干条）
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("{TableId}/{CustomerId}")]
        public Book? Get(decimal TableId, decimal CustomerId)
        {
            return _bookRepository.Get(TableId, CustomerId);
        }

        /// <summary>
        /// 预定表 返回所有桌位预定信息（若干条）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Book>? GetAll()
        {
            return _bookRepository.GetAll();
        }

        /// <summary>
        /// 预定表 查询状态有效的所有信息（若干条）
        /// </summary>
        /// <param name="TableId"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Book> GetAvailableBook()
        {
            return _bookRepository.GetAvailableBook();
        }

        /// <summary>
        /// 预定表 主码：TableId、CustomerId、BookTime
        /// </summary>
        /// <param name="newBook"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Update(Book newBook)
        {
            return _bookRepository.Update(newBook);
        }

        [HttpDelete("{TableId}/{CustomerId}")]
        public bool Delete(decimal TableId, decimal CustomerId)
        {
            return _bookRepository.Delete(TableId, CustomerId);
        }

        [HttpPost]
        public bool Add(Book book)
        {
            return _bookRepository.Add(book);
        }

    }
}
