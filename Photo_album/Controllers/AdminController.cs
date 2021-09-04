using System.Threading.Tasks;
using System.Web.Mvc;
using Photo_album.BLL.Services.Abstract;

namespace Photo_album.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public AdminController(IUserService userService, IPostService postService, ICommentService commentService)
        {
            _userService = userService;
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(_userService.Get());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteUser(string userKey)
        {
            await _userService.DeleteByKeyAsync(userKey);
            return RedirectToAction("Index",  await _userService.GetAsync());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeletePost(string postKey)
        {
            await _postService.DeleteByKeyAsync(postKey);
            return RedirectToAction("Index", await _userService.GetAsync());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteComment(string commentKey)
        {
            await _commentService.DeleteByKeyAsync(commentKey);
            return RedirectToAction("Index", await _userService.GetAsync());
        }
    }
}
