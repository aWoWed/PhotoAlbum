using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Photo_album.BLL.Services.Abstract;
using Photo_album.BLL.Services.Concrete;
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

            return View();
        }

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
