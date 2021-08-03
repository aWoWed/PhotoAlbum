using System.Collections.Generic;
using System.Threading.Tasks;
using Photo_album.Models.Entities;
using Photo_album.Models.Repositories.Abstract;

namespace Photo_album.Models.Repositories.EntityFramework
{
    public class PostRepository : IPostRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        public PostRepository(Photo_albumDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Post> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public Post GetByKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> GetByKeyAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Add(Post asset)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddAsync(Post Asset)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Post asset)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(Post asset)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteByKey(string index)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteByKeyAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
