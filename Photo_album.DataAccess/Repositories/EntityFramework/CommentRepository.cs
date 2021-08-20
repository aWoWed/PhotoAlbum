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
    public class CommentRepository : ICommentRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        public CommentRepository(Photo_albumDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Gets Comments from Db
        /// </summary>
        /// <returns>Comments from Db</returns>
        public IQueryable<Comment> Get() => _appDbContext.Comments;

        /// <summary>
        /// Gets Async Comments from Db
        /// </summary>
        /// <returns>Comments from Db</returns>
        public Task<IQueryable<Comment>> GetAsync() => Task.FromResult(_appDbContext.Comments.AsQueryable());

        /// <summary>
        /// Gets Comments by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns> Comments with current key</returns>
        public Comment GetByKey(string key) =>
            _appDbContext.Comments.FirstOrDefault(comment => comment.Id == key);

        /// <summary>
        /// Gets Async Comments by current key
        /// </summary>
        /// <param name="key"></param>
        /// <returns> Comments with current key</returns>
        public async Task<Comment> GetByKeyAsync(string key) => await _appDbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == key);

        /// <summary>
        /// Gets Comments by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns> Comments with current User key</returns>
        public IQueryable<Comment> GetByUserKey(string userKey) =>
            _appDbContext.Comments.Where(comment => comment.UserId == userKey);

        /// <summary>
        /// Gets Async Comments by current User key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns> Comments with current User key</returns>
        public Task<IQueryable<Comment>> GetByUserKeyAsync(string userKey) =>
            Task.FromResult(_appDbContext.Comments.Where(comment => comment.UserId == userKey));

        /// <summary>
        /// Gets Comments by contains Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Comments with contains text</returns>
        public IQueryable<Comment> GetByContainsText(string text) =>
            _appDbContext.Comments.Where(comment => comment.Text.ToLower().Contains(text.ToLower()));


        /// <summary>
        /// Gets Async Comments by contains Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Comments with contains text</returns>
        public Task<IQueryable<Comment>> GetByContainsTextAsync(string text) =>
            Task.FromResult(_appDbContext.Comments.Where(comment => comment.Text.ToLower().Contains(text.ToLower())));

        /// <summary>
        /// Inserts comment to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(Comment entity) => _appDbContext.Entry(entity).State = EntityState.Added;

        /// <summary>
        /// Updates comment to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Comment entity)
        {
            _appDbContext.Comments.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes Comment with current key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key) => _appDbContext.Comments.Remove(new Comment {Id = key});

        /// <summary>
        /// Deletes all Comments
        /// </summary>
        public void DeleteAll() => _appDbContext.Comments.RemoveRange(_appDbContext.Comments);
    }
}
