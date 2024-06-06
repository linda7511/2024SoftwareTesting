using DbOracle.Models;

namespace DbOracle.Repository
{
    public interface IPostRepository
    {
        public Post? Get(decimal id);

        public bool Add(Post post);

        public bool Delete(decimal id);

        public IEnumerable<Post>? GetAll();

        public IEnumerable<Post>? GetByDeptID(decimal departmentId);

        public bool Update(Post post);
    }
}
