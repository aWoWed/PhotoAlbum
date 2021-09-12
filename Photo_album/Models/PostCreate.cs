using System.Web;

namespace Photo_album.Models
{
    /// <summary>
    ///     Represents model for creating post
    /// </summary>
    public class PostCreate
    {
        /// <summary>
        ///     Gets or sets post description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets post image
        /// </summary>
        public HttpPostedFileBase Image { get; set; }
    }
}
