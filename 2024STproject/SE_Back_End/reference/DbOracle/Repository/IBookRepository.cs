using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IBookRepository
    {
        public bool Add(Book book);

        public bool Delete(decimal TableId, decimal CustomerId);

        public bool Update(Book newBook);

        public Book? Get(decimal TableId, decimal CustomerId);

        public IEnumerable<Book> GetAvailableBook();

        public IEnumerable<Book>? GetAll();
    }
}
