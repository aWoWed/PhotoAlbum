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
    }
}
