using System.Web;

namespace Photo_album.Models
{
    public class PostCreate
    {
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
