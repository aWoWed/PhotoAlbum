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

        /// <summary>
        ///     Gets All likeDTOs by user and post keys from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="postKey"></param>
        /// <returns>All likeDTOs by user and post keys</returns>
        IQueryable<LikeDTO> GetByUserPostKey(string userKey, string postKey);

        /// <summary>
        ///     Gets Async All likeDTOs by user and post keys from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="postKey"></param>
        /// <returns>All likeDTOs by user and post keys</returns>
        Task<IQueryable<LikeDTO>> GetByUserPostKeyAsync(string userKey, string postKey);

        /// <summary>
        ///     Gets All likeDTOs by user, post, comment keys from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="postKey"></param>
        /// <param name="commentKey"></param>
        /// <returns>All likeDTOs by user, post, comment keys</returns>
        IQueryable<LikeDTO> GetByUserPostCommentKey(string userKey, string postKey, string commentKey);

        /// <summary>
        ///     Gets Async All likes by user, post, comment keys from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="postKey"></param>
        /// <param name="commentKey"></param>
        /// <returns>All likeDTOs by user, post, comment keys</returns>
        Task<IQueryable<LikeDTO>> GetByUserPostCommentKeyAsync(string userKey, string postKey, string commentKey);
    }
}
