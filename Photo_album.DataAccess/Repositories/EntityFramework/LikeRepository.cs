using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Context;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;

namespace Photo_album.DataAccess.Repositories.EntityFramework
{
    /// <summary>
    ///     Represents like repository
    /// </summary>
    public class LikeRepository : ILikeRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        /// <summary>
        ///     Creates like repository instance
        /// </summary>
        /// <param name="appDbContext"></param>
        public LikeRepository(Photo_albumDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        ///     Gets Likes from Db
        /// </summary>
        /// <returns>Likes from Db</returns>
        public IQueryable<Like> Get() => _appDbContext.Likes;

        /// <summary>
        ///     Gets Async Likes from Db
        /// </summary>
        /// <returns>Likes from Db</returns>
        public Task<IQueryable<Like>> GetAsync() => Task.FromResult(_appDbContext.Likes.AsQueryable());

        /// <summary>
        ///     Gets Like by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Like with current key</returns>
        public Like GetByKey(string key) => _appDbContext.Likes.FirstOrDefault(like => like.Id == key);

        /// <summary>
        ///     Gets Async Like by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Like with current key</returns>
        public async Task<Like> GetByKeyAsync(string key) =>
            await _appDbContext.Likes.FirstOrDefaultAsync(like => like.Id == key);

        /// <summary>
        ///     Gets likes by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Likes with current User key</returns>
        public IQueryable<Like> GetByUserKey(string userKey) => _appDbContext.Likes.Where(like => like.UserId == userKey);

        /// <summary>
        ///     Gets Async likes by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Likes with current User key</returns>
        public Task<IQueryable<Like>> GetByUserKeyAsync(string userKey) =>
            Task.FromResult(_appDbContext.Likes.Where(like => like.UserId == userKey));

        /// <summary>
        ///     Inserts like to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(Like entity) => _appDbContext.Entry(entity).State = EntityState.Added;

        /// <summary>
        ///     Updates like to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Like entity) => _appDbContext.Entry(entity).State = EntityState.Modified;

        /// <summary>
        ///     Deletes like with current key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key) => _appDbContext.Likes.Remove(new Like {Id = key});

        /// <summary>
        ///     Deletes Async like with current key
        /// </summary>
        /// <param name="key"></param>
        public Task DeleteByKeyAsync(string key) => Task.FromResult(_appDbContext.Likes.Remove(new Like {Id = key}));

        /// <summary>
        ///     Deletes all Likes
        /// </summary>
        public void DeleteAll() => _appDbContext.Likes.RemoveRange(_appDbContext.Likes);

        /// <summary>
        ///     Gets All likes by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>Likes with current post key from Db</returns>
        public IQueryable<Like> GetByPostKey(string postKey) =>
            _appDbContext.Likes.Where(like => like.PostId == postKey);

        /// <summary>
        ///     Gets Async All likes by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>Likes with current post key from Db</returns>
        public Task<IQueryable<Like>> GetByPostKeyAsync(string postKey) =>
            Task.FromResult(_appDbContext.Likes.Where(like => like.PostId == postKey));

        /// <summary>
        ///     Gets All likes by comment key from Db
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>Likes with current comment key from Db</returns>
        public IQueryable<Like> GetByCommentKey(string commentKey) =>
            _appDbContext.Likes.Where(like => like.CommentId == commentKey);

        /// <summary>
        ///     Gets Async All likes by comment key from Db
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>Likes with current comment key from Db</returns>
        public Task<IQueryable<Like>> GetByCommentKeyAsync(string commentKey) =>
            Task.FromResult(_appDbContext.Likes.Where(like => like.CommentId == commentKey));
    }
}
