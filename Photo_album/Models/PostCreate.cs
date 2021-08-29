using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Photo_album.Models
{
    public class PostCreate
    {
        public string Description { get; set; }
        [Required]
        public HttpPostedFileBase Image { get; set; }
    }
}
