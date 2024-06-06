using DbOracle.Models;
using DbOracle.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLBookRepository : IBookRepository
    {
        private MyDbContext _context;

        public SQLBookRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }

        public bool Delete(decimal TableId, decimal CustomerId)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(a => a.TableId == TableId && a.CustomerId == CustomerId);
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return false;
            }
            return true;
        }

        public Book? Get(decimal TableId, decimal CustomerId)
        {
            try
            {
                Book book = _context.Books.FirstOrDefault(a => a.TableId == TableId && a.CustomerId == CustomerId);
                if (book == null)
                {
                    return null;
                }
                return book;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Book> GetAvailableBook()
        {
            try
            {
                var availableBooks = _context.Books.Where(a => a.BookStatus == "预定成功");
                return availableBooks;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Book>? GetAll()
        {
            try
            {
                return _context.Books;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Update(Book newBook)
        {
            try
            {
                var existedBook = _context.Books.FirstOrDefault(a => a.TableId == newBook.TableId && a.CustomerId == newBook.CustomerId
                                                                && a.BookTime == newBook.BookTime);
                if (existedBook == null)
                {
                    Console.WriteLine("无对应的预定信息 更新失败");
                    return false;
                }
                Type bookType = typeof(Book);
                PropertyInfo[] properties = bookType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(newBook);
                    if (value != null)
                    {
                        property.SetValue(existedBook, value);
                    }
                }
                _context.Books.Update(existedBook);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
