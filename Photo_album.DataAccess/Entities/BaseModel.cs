using System;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.DataAccess.Entities
{
    public abstract class BaseModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        protected BaseModel()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
