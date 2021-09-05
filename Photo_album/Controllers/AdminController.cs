using System.Threading.Tasks;
using System.Web.Mvc;
using Photo_album.BLL.Services.Abstract;

namespace Photo_album.Controllers
{
    /// <summary>
    ///     Represents admin controller
    /// </summary>
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        /// <summary>
        ///     Creates a new instance of the <see cref="AdminController" /> class
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="postService"></param>
        /// <param name="commentService"></param>
        public AdminController(IUserService userService, IPostService postService, ICommentService commentService)
        {
            _userService = userService;
            _postService = postService;
            _commentService = commentService;
        }

        /// <summary>
        ///     Gets all users except current admin
        /// </summary>
        /// <returns>All users except current admin</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(_userService.Get());
        }

        /// <summary>
        ///     Deletes all user info from DB
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>All users except current admin</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteUser(string userKey)
        {
            await _userService.DeleteByKeyAsync(userKey);
            return RedirectToAction("Index",  await _userService.GetAsync());
        }

        /// <summary>
        ///     Deletes all post info from DB
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>All users except current admin</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeletePost(string postKey)
        {
            await _postService.DeleteByKeyAsync(postKey);
            return RedirectToAction("Index", await _userService.GetAsync());
        }

        /// <summary>
        ///     Deletes all comment info from DB
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>All users except current admin</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteComment(string commentKey)
        {
            await _commentService.DeleteByKeyAsync(commentKey);
            return RedirectToAction("Index", await _userService.GetAsync());
        }
    }
}
