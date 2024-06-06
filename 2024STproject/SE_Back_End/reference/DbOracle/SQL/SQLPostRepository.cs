using DbOracle.Models;
using DbOracle.Repository;
using System.Reflection;

namespace DbOracle.SQL
{
    public class SQLPostRepository : IPostRepository
    {
        private MyDbContext _context;

        public SQLPostRepository(MyDbContext context)
        {
            _context = context;
        }

        public bool Add(Post post)
        {
            try
            {
                _context.Posts.Add(post);
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

        public bool Delete(decimal id)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(a => a.PostId == id);
                _context.Posts.Remove(post);
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

        public Post? Get(decimal id)
        {
            try
            {
                Post post = _context.Posts.FirstOrDefault(a => a.PostId == id);
                if (post == null)
                {
                    return null;
                }
                return post;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Post>? GetAll()
        {
            try
            {
                var posts = _context.Posts;
                if (posts == null)
                {
                    return null;
                }
                return posts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public IEnumerable<Post>? GetByDeptID(decimal departmentId)
        {
            try
            {
                var posts = _context.Posts.Where(a => a.DepartmentId == departmentId);
                if (posts == null)
                {
                    return null;
                }
                return posts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                return null;
            }
        }

        public bool Update(Post new_post)
        {
            try
            {
                //这里是利用反射写的
                var old_post = _context.Posts.Find(new_post.PostId);
                Type postType = typeof(Post);
                PropertyInfo[] properties = postType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(new_post);
                    if (value != null)
                    {
                        property.SetValue(old_post, value);
                    }
                }
                _context.Posts.Update(old_post);
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

    }
}
