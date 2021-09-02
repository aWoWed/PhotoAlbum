using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Photo_album.DataAccess.Entities
{
    /// <summary>
    ///     Represents user
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        ///     Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets profile photo
        /// </summary>
        public string ProfilePhoto { get; set; }

        /// <summary>
        ///     Gets or sets posts
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }

        /// <summary>
        ///     Gets or sets likes
        /// </summary>
        public virtual ICollection<Like> Likes { get; set; }

        /// <summary>
        ///     Gets or sets comments
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
    }
}