using System.Linq;
using System.Threading.Tasks;
using Photo_album.BLL.DTOs;

namespace Photo_album.BLL.Services.Abstract
{
    /// <summary>
    ///     Provides interface for like service
    /// </summary>
    public interface ILikeService : IService<string, LikeDTO>
    {
        /// <summary>
        ///     Gets All likeDTOs by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>LikeDTOs with current post key from Db</returns>
        IQueryable<LikeDTO> GetByPostKey(string postKey);

        /// <summary>
        ///     Gets Async All likeDTOs by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>LikeDTOs with current post key from Db</returns>
        Task<IQueryable<LikeDTO>> GetByPostKeyAsync(string postKey);

        /// <summary>
        ///     Gets All likeDTOs by comment key from Db
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>LikeDTOs with current comment key from Db</returns>
        IQueryable<LikeDTO> GetByCommentKey(string commentKey);

        /// <summary>
        ///     Gets Async All likeDTOs by comment key from Db
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>LikeDTOs with current comment key from Db</returns>
        Task<IQueryable<LikeDTO>> GetByCommentKeyAsync(string commentKey);
    }
}
