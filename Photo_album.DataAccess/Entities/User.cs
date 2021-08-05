using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Photo_album.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string Description { get; set; }
        public string ProfilePhoto { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}