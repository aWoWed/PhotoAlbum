using System;
using System.ComponentModel.DataAnnotations;

namespace Photo_album.DataAccess.Entities
{
    /// <summary>
    /// Represents base entity. Contains shared properties for all entities
    /// </summary>
    public abstract class BaseModel
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
        protected BaseModel()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
    }
}
