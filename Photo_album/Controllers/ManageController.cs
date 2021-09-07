using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Photo_album.BLL.Services.Abstract;
using Photo_album.Models.UserModels;

namespace Photo_album.Controllers
{
    /// <summary>
    ///     Represents manage controller
    /// </summary>
    public class ManageController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        ///      Creates a new instance of the <see cref="ManageController" /> class
        /// </summary>
        /// <param name="userService"></param>
        public ManageController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     User profile page
        /// </summary>
        /// <returns>User profile page view</returns>
        [HttpGet]
        public async Task<ActionResult> Index(string userKey)
        {
            return View(await _userService.FindUserByKeyAsync(string.IsNullOrEmpty(userKey) ? User.Identity.GetUserId() : userKey));
        }

        /// <summary>
        ///     Changes user password
        /// </summary>
        /// <returns>Change password view</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        ///     Changes user password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User profile view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (model.OldPassword == model.NewPassword)
                ModelState.AddModelError("", "Old password and new password are same!");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var result = await _userService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userService.FindUserByKeyAsync(userId);
                if (user != null)
                {
                    await _userService.Authenticate(user);
                }
                return RedirectToAction("Index");
            }
            AddErrors(result);
            return View(model);
        }

        /// <summary>
        ///     Changes profile info
        /// </summary>
        /// <returns>Change profile info view</returns>
        [HttpGet]
        public ActionResult ChangeProfileInfo()
        {
            return View();
        }

        /// <summary>
        ///     Changes profile info
        /// </summary>
        /// <param name="model"></param>
        /// <returns>User profile view</returns>
        [HttpPost]
        public async Task<ActionResult> ChangeProfileInfo(ChangeProfileInfo model)
        {
            var userDto = await _userService.FindUserByKeyAsync(User.Identity.GetUserId());

            userDto.Description = model.Description;

            if (model.ProfilePhoto != null)
            {
                var bytes = new byte[model.ProfilePhoto.ContentLength];
                await model.ProfilePhoto.InputStream.ReadAsync(bytes, 0, bytes.Length);
                var base64 = Convert.ToBase64String(bytes);
                userDto.ProfilePhoto = base64;
            }
            else
            {
                userDto.ProfilePhoto = null;
            }

            await _userService.UpdateUserInfo(userDto);

            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Adds error into identity result
        /// </summary>
        /// <param name="result"></param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
