using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Entities;

namespace Photo_album.DataAccess.Repositories.Abstract
{
    /// <summary>
    ///     Provides interface for like repository
    /// </summary>
    public interface ILikeRepository : IRepository<string, Like>
    {
        /// <summary>
        ///     Gets All likes by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>Likes with current post key from Db</returns>
        IQueryable<Like> GetByPostKey(string postKey);

        /// <summary>
        ///     Gets Async All likes by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>Likes with current post key from Db</returns>
        Task<IQueryable<Like>> GetByPostKeyAsync(string postKey);

        /// <summary>
        ///     Gets All likes by comment key from Db
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>Likes with current comment key from Db</returns>
        IQueryable<Like> GetByCommentKey(string commentKey);

        /// <summary>
        ///     Gets Async All likes by comment key from Db
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>Likes with current comment key from Db</returns>
        Task<IQueryable<Like>> GetByCommentKeyAsync(string commentKey);
    }
}
