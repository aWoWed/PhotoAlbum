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
    /// <summary>
    ///     Represents user controller
    /// </summary>
    [Authorize]
    public class UserController: Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationManager _authenticationManager;

        /// <summary>
        ///     Creates a new instance of the <see cref="UserController" /> class
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="authenticationManager"></param>
        public UserController(IUserService userService, IAuthenticationManager authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

        /// <summary>
        ///     Logs in a user
        /// </summary>
        /// <returns>Login view</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///     Logs in a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Home view with logged in user</returns>
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

        /// <summary>
        ///     Logs out a user
        /// </summary>
        /// <returns>Home view</returns>
        public ActionResult Logout()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Registers a user
        /// </summary>
        /// <returns>Register view</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        ///     Registers a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Home view</returns>
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

        /// <summary>
        ///     Sets admin initial data
        /// </summary>
        /// <returns>Admin</returns>
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