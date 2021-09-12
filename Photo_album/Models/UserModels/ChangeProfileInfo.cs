using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Photo_album.Models.UserModels
{
    /// <summary>
    ///     Represents model for changing profile info
    /// </summary>
    public class ChangeProfileInfo
    {
        /// <summary>
        ///     Gets or sets profile photo
        /// </summary>
        [Display(Name = "Profile photo")]
        public HttpPostedFileBase ProfilePhoto { get; set; }

        /// <summary>
        ///     Gets or sets profile descripation
        /// </summary>
        [Display(Name = "Profile description")]
        public string Description { get; set; }
    }
}
