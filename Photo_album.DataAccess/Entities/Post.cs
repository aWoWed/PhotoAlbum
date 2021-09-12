using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_album.DataAccess.Entities
{
    /// <summary>
    ///     Represents post
    /// </summary>
    public class Post : BaseModel
    {
        /// <summary>
        ///     Gets or sets image
        /// </summary>
        [Required]
        public string Image { get; set; }

        /// <summary>
        ///     Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets user id
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        ///     Gets or sets user
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        ///     Gets or sets comments
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        ///     Gets or sets likes
        /// </summary>
        public virtual ICollection<Like> Likes { get; set; }
    }
}