using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Entities;

namespace Photo_album.DataAccess.Repositories.Abstract
{
    /// <summary>
    ///     Provides interface for post repository
    /// </summary>
    public interface IPostRepository : IRepository<string, Post>
    {
        /// <summary>
        ///     Gets All post elems, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All post elems, which contain current text, from Db</returns>
        IQueryable<Post> GetByContainsText(string text);

        /// <summary>
        ///     Gets Async All post elems, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All post elems, which contain current text, from Db</returns>
        Task<IQueryable<Post>> GetByContainsTextAsync(string text);

        /// <summary>
        /// Counts posts, which user with userKey made
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Number of posts made by user with userKey</returns>
        int Count(string userKey);
    }
}
