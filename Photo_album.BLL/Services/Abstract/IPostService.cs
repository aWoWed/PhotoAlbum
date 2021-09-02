using System.Linq;
using System.Threading.Tasks;
using Photo_album.BLL.DTOs;

namespace Photo_album.BLL.Services.Abstract
{
    /// <summary>
    ///     Provides interface for post service
    /// </summary>
    public interface IPostService : IService<string, PostDTO>
    {
        /// <summary>
        ///     Gets All postDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All postDTOs, which contain current text, from Db</returns>
        IQueryable<PostDTO> GetByContainsText(string text);

        /// <summary>
        ///     Gets Async All postDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All postDTOs, which contain current text, from Db</returns>
        Task<IQueryable<PostDTO>> GetByContainsTextAsync(string text);
    }
}