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
    }
}
