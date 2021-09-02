using System.Linq;
using System.Threading.Tasks;
using Photo_album.BLL.DTOs;

namespace Photo_album.BLL.Services.Abstract
{
    /// <summary>
    ///     Provides interface for comment service
    /// </summary>
    public interface ICommentService : IService<string, CommentDTO>
    {
        /// <summary>
        ///     Gets All commentDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All commentDTOs, which contain current text, from Db</returns>
        IQueryable<CommentDTO> GetByContainsText(string text);

        /// <summary>
        ///     Gets Async All commentDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns> All commentDTOs, which contain current text, from Db</returns>
        Task<IQueryable<CommentDTO>> GetByContainsTextAsync(string text);
    }
}