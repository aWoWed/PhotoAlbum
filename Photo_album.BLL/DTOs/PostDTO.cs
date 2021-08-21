using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Photo_album.DataAccess.Entities;

namespace Photo_album.BLL.DTOs
{
    public class PostDTO : BaseDTO
    {
        [Required]
        public string Image { get; set; }
        
        public uint Likes { get; set; }
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        public UserDTO UserDto { get; set; }
        public ICollection<CommentDTO> CommentDtos { get; set; }

        public PostDTO()
        {
            CommentDtos = new List<CommentDTO>();
        }
    }
}
