using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Context;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;

namespace Photo_album.DataAccess.Repositories.EntityFramework
{
    /// <summary>
    ///     Represents post repository
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        /// <summary>
        ///     Creates post repository instance
        /// </summary>
        /// <param name="appDbContext"></param>
        public PostRepository(Photo_albumDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        ///     Gets Posts from Db
        /// </summary>
        /// <returns>Posts from Db</returns>
        public IQueryable<Post> Get() => _appDbContext.Posts;

        /// <summary>
        ///     Gets Async Posts from Db
        /// </summary>
        /// <returns>Posts from Db</returns>
        public Task<IQueryable<Post>> GetAsync() => Task.FromResult(_appDbContext.Posts.AsQueryable());

        /// <summary>
        ///     Gets Post by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Posts with current key</returns>
        public Post GetByKey(string key) => _appDbContext.Posts.FirstOrDefault(post => post.Id == key);

        /// <summary>
        ///     Gets Async Post by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Posts with current key</returns>
        public async Task<Post> GetByKeyAsync(string key) => await _appDbContext.Posts.FirstOrDefaultAsync(post => post.Id == key);

        /// <summary>
        ///     Gets Posts by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Posts with current User key</returns>
        public IQueryable<Post> GetByUserKey(string userKey) => _appDbContext.Posts.Where(post => post.UserId == userKey);

        /// <summary>
        ///     Gets Async Posts by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Posts with current User key</returns>
        public Task<IQueryable<Post>> GetByUserKeyAsync(string userKey) =>
            Task.FromResult(_appDbContext.Posts.Where(post => post.UserId == userKey));

        /// <summary>
        ///     Gets Posts by contains Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Posts with contains text</returns>
        public IQueryable<Post> GetByContainsText(string text) =>
            _appDbContext.Posts.Where(post => post.Description.ToLower().Contains(text.ToLower()));

        /// <summary>
        ///     Gets Async Posts by contains Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Posts with contains text</returns>
        public Task<IQueryable<Post>> GetByContainsTextAsync(string text) =>
            Task.FromResult(_appDbContext.Posts.Where(post => post.Description.ToLower().Contains(text.ToLower())));

        /// <summary>
        ///     Inserts comment to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(Post entity) => _appDbContext.Entry(entity).State = EntityState.Added;

        /// <summary>
        ///     Updates comment to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Post entity) => _appDbContext.Entry(entity).State = EntityState.Modified;
        
        /// <summary>
        ///     Deletes comment with current key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key) => _appDbContext.Posts.Remove(new Post {Id = key});

        /// <summary>
        ///     Deletes Async comment with current key
        /// </summary>
        /// <param name="key"></param>
        public Task DeleteByKeyAsync(string key) => Task.FromResult(_appDbContext.Posts.Remove(new Post {Id = key}));

        /// <summary>
        ///     Deletes all Comments
        /// </summary>
        public void DeleteAll() => _appDbContext.Posts.RemoveRange(_appDbContext.Posts);

        /// <summary>
        ///     Counts posts, which user with userKey made
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Number of posts made by user with userKey</returns>
        public int Count(string userKey) => _appDbContext.Posts.Count(post => post.UserId == userKey);
    }
}
