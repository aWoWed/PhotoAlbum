using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_album.DataAccess.Entities
{
    public class Post : BaseModel
    {
        [Required]
        public string Image { get; set; }
        
        public uint Likes { get; set; }
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}