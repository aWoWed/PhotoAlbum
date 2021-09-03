using Photo_album.BLL.DTOs;

namespace Photo_album.ViewModels
{
    public class FullPostViewModel
    {

        /// <summary>
        ///     Gets or sets postDTO
        /// </summary>
        public PostDTO PostDto { get; set; }

        /// <summary>
        ///     Gets or sets a valued indicating is post liked by requesting user or not
        /// </summary>
        public bool IsLiked { get; set; }

    }
}
