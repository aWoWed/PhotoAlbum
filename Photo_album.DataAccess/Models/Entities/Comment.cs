using System;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.DataAccess.Models.Entities
{
    public class Comment : BaseModel
    {
        public string Text { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid PostId { get; set; }
    }
}
