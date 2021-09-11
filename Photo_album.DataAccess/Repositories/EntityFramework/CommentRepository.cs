using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Context;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;

namespace Photo_album.DataAccess.Repositories.EntityFramework
{
    /// <summary>
    ///     Represents comment repository
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        /// <summary>
        ///     Creates comment repository instance
        /// </summary>
        /// <param name="appDbContext"></param>
        public CommentRepository(Photo_albumDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        ///     Gets Comments from Db
        /// </summary>
        /// <returns>Comments from Db</returns>
        public IQueryable<Comment> Get() => _appDbContext.Comments;

        /// <summary>
        ///     Gets Async Comments from Db
        /// </summary>
        /// <returns>Comments from Db</returns>
        public Task<IQueryable<Comment>> GetAsync() => Task.FromResult(_appDbContext.Comments.AsQueryable());

        /// <summary>
        ///     Gets Comment by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Comment with current key</returns>
        public Comment GetByKey(string key) =>
            _appDbContext.Comments.FirstOrDefault(comment => comment.Id == key);

        /// <summary>
        ///     Gets Async Comment by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Comment with current key</returns>
        public async Task<Comment> GetByKeyAsync(string key) => await _appDbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == key);

        /// <summary>
        ///     Gets Comments by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Comments with current User key</returns>
        public IQueryable<Comment> GetByUserKey(string userKey) =>
            _appDbContext.Comments.Where(comment => comment.UserId == userKey);

        /// <summary>
        ///     Gets Async Comments by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Comments with current User key</returns>
        public Task<IQueryable<Comment>> GetByUserKeyAsync(string userKey) =>
            Task.FromResult(_appDbContext.Comments.Where(comment => comment.UserId == userKey));

        /// <summary>
        ///     Gets Comments by contains Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Comments with contains text</returns>
        public IQueryable<Comment> GetByContainsText(string text) =>
            _appDbContext.Comments.Where(comment => comment.Text.ToLower().Contains(text.ToLower()));


        /// <summary>
        ///     Gets Async Comments by contains Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Comments with contains text</returns>
        public Task<IQueryable<Comment>> GetByContainsTextAsync(string text) =>
            Task.FromResult(_appDbContext.Comments.Where(comment => comment.Text.ToLower().Contains(text.ToLower())));

        /// <summary>
        ///     Gets All comment elems, which contain post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>All comment elems, which contain post key from Db</returns>
        public IQueryable<Comment> GetByPostKey(string postKey) =>
            _appDbContext.Comments.Where(comment => comment.PostId == postKey);

        /// <summary>
        ///     Gets Async All comment elems, which contain post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>All comment elems, which contain post key from Db</returns>
        public Task<IQueryable<Comment>> GetByPostKeyAsync(string postKey) =>
            Task.FromResult(_appDbContext.Comments.Where(comment => comment.PostId == postKey));

        /// <summary>
        ///     Deletes comments by post id from DB
        /// </summary>
        /// <param name="postKey"></param>
        public void DeleteByPostKey(string postKey) => _appDbContext.Comments.RemoveRange(GetByPostKey(postKey));

        /// <summary>
        ///     Deletes Async comments by post id from DB
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>A <see cref="Task" /> representing asynchronous action result</returns>
        public async Task DeleteByPostKeyAsync(string postKey) =>
            await Task.FromResult(_appDbContext.Comments.RemoveRange(await GetByPostKeyAsync(postKey)));

        /// <summary>
        ///     Inserts comment to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(Comment entity) => _appDbContext.Entry(entity).State = EntityState.Added;

        /// <summary>
        ///     Updates comment to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Comment entity)
        {
            var comment = GetByKey(entity.Id);
            comment.Text = entity.Text;
        }

        /// <summary>
        ///     Deletes Comment with current key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key) => _appDbContext.Comments.Remove(GetByKey(key));

        public async Task DeleteByKeyAsync(string key) =>
            await Task.FromResult(_appDbContext.Comments.Remove(await GetByKeyAsync(key)));

        /// <summary>
        ///     Deletes all Comments
        /// </summary>
        public void DeleteAll() => _appDbContext.Comments.RemoveRange(_appDbContext.Comments);
    }
}
