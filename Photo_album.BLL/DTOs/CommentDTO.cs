using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Photo_album.DataAccess.Entities;

namespace Photo_album.BLL.DTOs
{
    public class CommentDTO : BaseDTO
    {
        public string Text { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string PostId { get; set; }
        public UserDTO UserDto { get; set; }
        public PostDTO PostDto { get; set; }
    }
}
