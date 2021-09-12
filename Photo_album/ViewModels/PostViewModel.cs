using System.Linq;
using Photo_album.BLL.DTOs;
using Photo_album.Models;

namespace Photo_album.ViewModels
{
    /// <summary>
    ///     Represents view model for posts
    /// </summary>
    public class PostViewModel
    {
        /// <summary>
        ///     Gets or sets page info
        /// </summary>
        public PageInfo PageInfo { get; set; }

        /// <summary>
        ///     Gets or sets posts
        /// </summary>
        public IQueryable<PostDTO> PostDTOs { get; set; }
    }
}
