using System.ComponentModel.DataAnnotations;

namespace Photo_album.Models.UserModels
{
    /// <summary>
    ///     Represents model for register
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        ///     Gets or sets email
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets confirm password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}