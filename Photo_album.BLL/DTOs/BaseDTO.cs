using System;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.BLL.DTOs
{
    /// <summary>
    ///     Represents base DTO. Contains shared properties for all DTOs
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        ///     Gets or sets id
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets creation date
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; }

        /// <summary>
        ///     Initializes default id and creation date
        /// </summary>
        protected BaseDTO()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
    }
}
