using System.Linq;
using System.Web.Mvc;
using Photo_album.BLL.Services.Abstract;
using Photo_album.Extensions;

namespace Photo_album.Controllers
{
    /// <summary>
    ///     Represents home controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        /// <summary>
        ///     Creates a new instance of the <see cref="HomeController" /> class
        /// </summary>
        /// <param name="postService"></param>
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        ///     Home page
        /// </summary>
        /// <param name="count"></param>
        /// <returns>Home page with random posts</returns>
        public ActionResult Index(int count = 12)
        {
            var posts = _postService.Get().ToList();
            if (count > posts.Count)
                count = posts.Count;
            return View(posts.RandomElements(count).AsQueryable());
        }
    }
}