using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_album.DataAccess.Entities
{
    /// <summary>
    ///     Represents like
    /// </summary>
    public class Like : BaseModel
    {
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
        ///     Gets or sets post id
        /// </summary>
        [Required]
        public string PostId { get; set; }

        /// <summary>
        ///     Gets or sets post
        /// </summary>
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}
