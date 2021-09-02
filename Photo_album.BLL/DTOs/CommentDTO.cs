using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.BLL.DTOs
{
    /// <summary>
    ///     Represents commentDTO
    /// </summary>
    public class CommentDTO : BaseDTO
    {
        /// <summary>
        ///     Gets or sets text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets userDTO id
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        ///     Gets or sets postDTO id
        /// </summary>
        [Required]
        public string PostId { get; set; }

        /// <summary>
        ///     Gets or sets userDTO
        /// </summary>
        public UserDTO UserDto { get; set; }

        /// <summary>
        ///     Gets or sets postDTO
        /// </summary>
        public PostDTO PostDto { get; set; }

        /// <summary>
        ///     Gets or sets likeDTOs
        /// </summary>
        public ICollection<LikeDTO> LikeDtos { get; set; }

        /// <summary>
        ///     Initializes default values of likeDTOs
        /// </summary>
        public CommentDTO()
        {
            LikeDtos = new List<LikeDTO>();
        }
    }
}
