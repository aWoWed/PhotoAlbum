using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Services.Abstract;
using Photo_album.Models;
using Photo_album.ViewModels;

namespace Photo_album.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService, IUserService userService, ICommentService commentService)
        {
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var postViewModel = new PostViewModel { PostDTOs = _postService.Get().OrderByDescending(post => post.CreationDate) };
            return View(postViewModel);
        }

        [HttpGet]
        public ActionResult FullPost(string id)
        {
            return View(_postService.GetByKey(id));
        }

        [HttpGet]
        public ActionResult OtherUserPosts(string userKey)
        {
            return View(_postService.GetByUserKey(userKey));
        }

        [HttpPost]
        public ActionResult AddLike()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult PostCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostCreate(PostCreate post)
        {
            if (string.IsNullOrEmpty(post.Description))
                ModelState.AddModelError("Description", "Error! Please enter your description!");

            if (post.Image == null)
                ModelState.AddModelError("Image", "Error! Please pick your image!");

            if (ModelState.IsValid)
            {
                var bytes = new byte[post.Image.ContentLength];
                await post.Image.InputStream.ReadAsync(bytes, 0, bytes.Length);
                var base64 = Convert.ToBase64String(bytes);
                var postDTO = new PostDTO
                {
                    Description = post.Description.Trim(), Image = base64, UserId = User.Identity.GetUserId()
                };
                await _postService.InsertAsync(postDTO);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(string postKey, string text)
        {
            if (string.IsNullOrEmpty(text))
                ModelState.AddModelError("Text", "Error! Please enter your text!");

            if (ModelState.IsValid)
            {
                var post = _postService.GetByKey(postKey);

                post.CommentDtos.Add(new CommentDTO
                {
                    Text = text,
                    PostId = postKey,
                    UserId = User.Identity.GetUserId(),
                    UserDto = _userService.FindUserByKey(User.Identity.GetUserId()),
                    PostDto = post,
                });

                await _postService.UpdateAsync(post);

                await _commentService.InsertAsync(new CommentDTO
                {
                    Text = text,
                    PostId = postKey,
                    UserId = User.Identity.GetUserId(),
                });

                ModelState.Clear();

                return View("FullPost", post);
            }

            return RedirectToAction("FullPost");
        }

        [HttpGet]
        public ActionResult PostsByUser()
        {
            return View(_postService.GetByUserKey(User.Identity.GetUserId()).OrderByDescending(post => post.CreationDate));
        }
    }
}
