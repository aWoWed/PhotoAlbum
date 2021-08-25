using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_album.DataAccess.Entities
{
    public class Comment : BaseModel
    {
        public string Text { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string PostId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}