using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Photo_album.Models.UserModels
{
    public class ChangeProfileInfo
    {
        [Display(Name = "Profile photo")]
        public HttpPostedFileBase ProfilePhoto { get; set; }
        [Display(Name = "Profile description")]
        public string Description { get; set; }
    }
}
