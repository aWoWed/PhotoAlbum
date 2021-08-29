using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Services.Abstract;
using Photo_album.Models.UserModels;

namespace Photo_album.Controllers
{
    [Authorize]
    public class UserController: Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationManager _authenticationManager;

        public UserController(IUserService userService, IAuthenticationManager authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

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
            if (!ModelState.IsValid) return View(model);
            var userDto = new UserDTO
            {
                UserName = model.UserName, Password = model.Password
            };
            var claim = await _userService.Authenticate(userDto);

            if (claim == null)
                ModelState.AddModelError("", "Your login or password are incorrect!");

            else
            {
                _authenticationManager.SignOut();
                _authenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                }, claim);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
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

            if (!ModelState.IsValid) return View(model);
            var userDto = new UserDTO
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                Role = new List<string>{"user"}
            };

            var operationDetails = await _userService.Create(userDto);

            if (operationDetails.Succeed)
                return RedirectToAction("Index","Home");

            ModelState.AddModelError(operationDetails.Property, operationDetails.Message);

            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserDTO
            {
                Email = "admin@gmail.com",
                UserName = "Admin",
                Password = "Admin123",
                Role = new List<string>{"admin"},
                
            }, new List<string> { "user", "admin" });
        }
    }

}