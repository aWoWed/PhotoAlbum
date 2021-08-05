using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Context;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;

namespace Photo_album.DataAccess.Repositories.EntityFramework
{
    public class PostRepository : IPostRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        public PostRepository(Photo_albumDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Post> Get() => _appDbContext.Posts;

        public async Task<IEnumerable<Post>> GetAsync() => await _appDbContext.Posts.ToListAsync();

        public Post GetByKey(string key) => _appDbContext.Posts.FirstOrDefault(post => post.Id == key);

        public async Task<Post> GetByKeyAsync(string key) => await _appDbContext.Posts.FirstOrDefaultAsync(post => post.Id == key);
        public IQueryable<Post> GetByUserKey(string userKey) => _appDbContext.Posts.Where(post => post.UserId == userKey);

        public Task<IQueryable<Post>> GetByUserKeyAsync(string userKey) =>
            new Task<IQueryable<Post>>(() => _appDbContext.Posts.Where(post => post.UserId == userKey));

        public IQueryable<Post> GetByContainsText(string text) =>
            _appDbContext.Posts.Where(post => post.Description.Contains(text));

        public Task<IQueryable<Post>> GetByContainsTextAsync(string text) =>
            new Task<IQueryable<Post>>(() => _appDbContext.Posts.Where(post => post.Description.Contains(text)));

        public void Save(Post entity) => _appDbContext.Entry(entity).State = entity.Id == default ? EntityState.Added : EntityState.Modified;

        public void DeleteByKey(string key) => _appDbContext.Posts.Remove(new Post {Id = key});

        public void DeleteAll() => _appDbContext.Posts.RemoveRange(_appDbContext.Posts);

        public int Count(string userKey) => _appDbContext.Posts.Count(post => post.UserId == userKey);
    }
}
