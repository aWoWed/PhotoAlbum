using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Entities;

namespace Photo_album.DataAccess.Repositories.Abstract
{
    /// <summary>
    ///     Provides interface for comment repository
    /// </summary>
    public interface ICommentRepository : IRepository<string, Comment>
    {
        /// <summary>
        ///     Gets All comment elems, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All comment elems, which contain current text, from Db</returns>
        IQueryable<Comment> GetByContainsText(string text);

        /// <summary>
        ///     Gets Async All comment elems, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All comment elems, which contain current text, from Db</returns>
        Task<IQueryable<Comment>> GetByContainsTextAsync(string text);

        /// <summary>
        ///     Gets All comment elems, which contain post id from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>All comment elems, which contain post id from Db</returns>
        IQueryable<Comment> GetByPostKey(string postKey);

        /// <summary>
        ///     Gets Async All comment elems, which contain post id from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>All comment elems, which contain post id from Db</returns>
        Task<IQueryable<Comment>> GetByPostKeyAsync(string postKey);

        /// <summary>
        ///     Deletes comments by post id from DB
        /// </summary>
        /// <param name="postKey"></param>
        void DeleteByPostKey(string postKey);

        /// <summary>
        ///     Deletes Async comments by post id from DB
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>A <see cref="Task" /> representing asynchronous action result</returns>
        Task DeleteByPostKeyAsync(string postKey);
    }
}
