using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.BLL.DTOs
{
    /// <summary>
    ///     Represents postDTO
    /// </summary>
    public class PostDTO : BaseDTO
    {
        /// <summary>
        ///     Gets or sets image
        /// </summary>
        [Required] 
        public string Image { get; set; }

        /// <summary>
        ///     Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets userDTO id
        /// </summary>
        [Required] 
        public string UserId { get; set; }

        /// <summary>
        ///     Gets or sets userDTO
        /// </summary>
        public UserDTO UserDto { get; set; }

        /// <summary>
        ///     Gets or sets commentDTOs
        /// </summary>
        public ICollection<CommentDTO> CommentDtos { get; set; }

        /// <summary>
        ///     Gets or sets likeDTOs
        /// </summary>
        public ICollection<LikeDTO> LikeDtos { get; set; }

        /// <summary>
        ///     Initializes default values of commentDTOs and likeDTOs
        /// </summary>
        public PostDTO()
        {
            CommentDtos = new List<CommentDTO>();
            LikeDtos = new List<LikeDTO>();
        }
    }
}
