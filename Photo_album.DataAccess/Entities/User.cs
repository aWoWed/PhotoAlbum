using Microsoft.AspNet.Identity.EntityFramework;

namespace Photo_album.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string Description { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
