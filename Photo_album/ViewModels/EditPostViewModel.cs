using Photo_album.BLL.DTOs;
using Photo_album.Models;

namespace Photo_album.ViewModels
{
    /// <summary>
    ///     Represents view model for editing post
    /// </summary>
    public class EditPostViewModel
    {
        /// <summary>
        ///     Gets or sets postDTO
        /// </summary>
        public PostDTO PostDto { get; set; }

        /// <summary>
        ///     Gets or sets post create model
        /// </summary>
        public PostCreate PostCreate { get; set; }
    }
}
