using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Services.Abstract;
using Photo_album.BLL.Services.Concrete;
using Photo_album.Models.UserModels;

namespace Photo_album.Controllers
{
    [Authorize]
    public class UserController: Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>() ?? new UserService();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                var userDto = new UserDTO { Email = model.Email, Password = model.Password };
                var claim = await UserService.Authenticate(userDto);

                if (claim == null)
                    ModelState.AddModelError("", "Your login or password are incorrect!");

                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                var userDto = new UserDTO
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Password = model.Password,
                    Role = new List<string>{"user"}
                };

                var operationDetails = await UserService.Create(userDto);

                if (operationDetails.Succeed)
                    return RedirectToAction("Index","Home");

                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "german123@gmail.com",
                UserName = "german123@gmail.com",
                Password = "German123",
                Role = new List<string>{"admin"},
                
            }, new List<string> { "user", "admin" });
        }
    }

}