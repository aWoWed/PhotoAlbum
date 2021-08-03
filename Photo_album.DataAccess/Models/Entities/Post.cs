using System;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.DataAccess.Models.Entities
{
    public class Post : BaseModel
    {
        public string Image { get; set; }
        public uint Likes { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
