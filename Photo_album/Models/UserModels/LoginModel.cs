using System.ComponentModel.DataAnnotations;

namespace Photo_album.Models.UserModels
{
    /// <summary>
    ///     Represents model for login
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        ///     Gets or sets user name
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets remember me
        /// </summary>
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

    }
}