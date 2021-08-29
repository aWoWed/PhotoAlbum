using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.BLL.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string ProfilePhoto { get; set; }
        public List <string> Role { get; set; }
        public ICollection<CommentDTO> CommentDtos { get; set; }
        public ICollection<PostDTO> PostDtos { get; set; }

        public UserDTO()
        {
            Id = Guid.NewGuid().ToString();
            CommentDtos = new List<CommentDTO>();
            PostDtos = new List<PostDTO>();
        }
    }
}
