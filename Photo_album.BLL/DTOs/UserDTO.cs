using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.BLL.DTOs
{
    /// <summary>
    ///     Represents userDTO
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        ///     Gets or sets id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        ///     Gets or sets email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets profile photo
        /// </summary>
        public string ProfilePhoto { get; set; }

        /// <summary>
        ///     Gets or sets roles
        /// </summary>
        public List <string> Role { get; set; }

        /// <summary>
        ///     Gets or sets commentDTOs
        /// </summary>
        public ICollection<CommentDTO> CommentDtos { get; set; }

        /// <summary>
        ///     Gets or sets PostDTOs
        /// </summary>
        public ICollection<PostDTO> PostDtos { get; set; }

        /// <summary>
        ///     Gets or sets LikeDTOs
        /// </summary>
        public ICollection<LikeDTO> LikeDtos { get; set; }

        /// <summary>
        ///     Initializes default values of id, commentDTOs, postDTOs, likeDTOs
        /// </summary>
        public UserDTO()
        {
            Id = Guid.NewGuid().ToString();
            CommentDtos = new List<CommentDTO>();
            PostDtos = new List<PostDTO>();
            LikeDtos = new List<LikeDTO>();
        }

    }
}
