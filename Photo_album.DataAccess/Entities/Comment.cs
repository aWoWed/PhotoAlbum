using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_album.DataAccess.Entities
{
    /// <summary>
    ///     Represents comment
    /// </summary>
    public class Comment : BaseModel
    {
        /// <summary>
        ///     Gets or sets text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets user id
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        ///     Gets or sets post id
        /// </summary>
        [Required]
        public string PostId { get; set; }

        /// <summary>
        ///     Gets or sets user
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        ///     Gets or sets post
        /// </summary>
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        /// <summary>
        ///     Gets or sets likes
        /// </summary>
        public virtual ICollection<Like> Likes { get; set; }
    }
}