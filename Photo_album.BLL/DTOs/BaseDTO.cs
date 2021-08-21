using System;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.BLL.DTOs
{
    public class BaseDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        protected BaseDTO()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
    }
}
