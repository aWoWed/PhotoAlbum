namespace Photo_album.BLL.DTOs
{
    /// <summary>
    ///     Represents likeDTO
    /// </summary>
    public class LikeDTO : BaseDTO
    {
        /// <summary>
        ///     Gets or sets userDTO id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     Gets or sets userDTO
        /// </summary>
        public UserDTO UserDto { get; set; }

        /// <summary>
        ///     Gets or sets postDTO id
        /// </summary>
        public string PostId { get; set; }

        /// <summary>
        ///     Gets or sets postDTO
        /// </summary>
        public PostDTO PostDto { get; set; }

        /// <summary>
        ///     Gets or sets commentDTO id
        /// </summary>
        public string CommentId { get; set; }

        /// <summary>
        ///     Gets or sets commentDTO
        /// </summary>
        public CommentDTO CommentDto { get; set; }
    }
}
