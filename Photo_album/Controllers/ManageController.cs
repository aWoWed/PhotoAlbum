using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Photo_album.BLL.Services.Abstract;
using Photo_album.Models.UserModels;

namespace Photo_album.Controllers
{
    public class ManageController : Controller
    {
        private readonly IUserService _userService;

        public ManageController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                    : "";

            var user = await _userService.FindUserByKeyAsync(User.Identity.GetUserId());

            return View(user);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userService.FindUserByKeyAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await _userService.Authenticate(user);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeProfileInfo()
        {
            return View();
        }

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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess
        }

    }
}
