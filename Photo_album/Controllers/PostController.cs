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
        private readonly ILikeService _likeService;

        public PostController(IPostService postService, IUserService userService, ICommentService commentService, ILikeService likeService)
        {
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
            _likeService = likeService;
        }

        [HttpGet]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var postViewModel = new PostViewModel
            {
                PostDTOs = string.IsNullOrEmpty(searchString)
                    ? _postService.Get().OrderByDescending(post => post.CreationDate).Skip((page - 1) * pageSize)
                        .Take(pageSize)
                    : _postService.GetByContainsText(searchString).OrderByDescending(post => post.CreationDate)
                        .Skip((page - 1) * pageSize).Take(pageSize),
                PageInfo = new PageInfo
                {
                    PageNumber = page, PageSize = pageSize,
                    TotalItems = string.IsNullOrEmpty(searchString)
                        ? _postService.Get().Count()
                        : _postService.GetByContainsText(searchString).Count()
                }
            };
            return View(postViewModel);
        }

        [HttpGet]
        public ActionResult FullPost(string id)
        {
            var isLiked = _likeService.GetByUserPostKey(User.Identity.GetUserId(), id).Any();
            var fullPostViewModel = new FullPostViewModel{ IsLiked = isLiked, PostDto = _postService.GetByKey(id) };
            return View(fullPostViewModel);
        }

        /// <summary>
        ///     Adds a like to the post
        /// </summary>
        /// <param name="postKey">Post id</param>
        /// <returns>A <see cref="Task" /> representing asynchronous action result</returns>
        [HttpGet]
        public async Task<ActionResult> LikePost(string postKey)
        {
            var postDto = await _postService.GetByKeyAsync(postKey);
            await _likeService.InsertAsync(new LikeDTO
            {
                PostId = postKey,
                UserId = User.Identity.GetUserId(),
            });
            await _postService.UpdateAsync(postDto);
            return RedirectToAction("FullPost", new {id = postKey});
        }

        /// <summary>
        ///     Deletes a like from the post
        /// </summary>
        /// <param name="postKey">Post id</param>
        /// <returns>A <see cref="Task" /> representing asynchronous action result</returns>
        [HttpGet]
        public async Task<ActionResult> UnlikePost(string postKey)
        {
            var postDto = await _postService.GetByKeyAsync(postKey);
            var likeKey = _likeService.GetByUserPostKey(User.Identity.GetUserId(), postKey).Single().Id;
            await _likeService.DeleteByKeyAsync(likeKey);
            await _postService.UpdateAsync(postDto);
            return RedirectToAction("FullPost", new { id = postKey });
        }


        [HttpGet]
        public ActionResult OtherUserPosts(string userKey, int page = 1, int pageSize = 10)
        {
            var postViewModel = new PostViewModel
            {
                PostDTOs = _postService.GetByUserKey(userKey).OrderByDescending(post => post.CreationDate).Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _postService.Get().Count() }
            };
            return View(postViewModel);
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
                return RedirectToAction("PostsByUser");
            }

            return View("PostCreate");
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(string postKey, string text)
        {
            if (string.IsNullOrEmpty(text))
                ModelState.AddModelError("Text", "Error! Please enter your text!");

            var isLiked = _likeService.GetByUserPostKey(User.Identity.GetUserId(), postKey) != null;
            var fullPostViewModel = new FullPostViewModel { IsLiked = isLiked, PostDto = _postService.GetByKey(postKey) };

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

                isLiked = _likeService.GetByUserPostKey(User.Identity.GetUserId(), postKey) != null;
                fullPostViewModel = new FullPostViewModel { IsLiked = isLiked, PostDto = post };

                ModelState.Clear();

                return View("FullPost", fullPostViewModel);
            }

            return View("FullPost", fullPostViewModel);
        }

        [HttpGet]
        public ActionResult PostsByUser(int page = 1, int pageSize = 10)
        {
            var postViewModel = new PostViewModel
            {
                PostDTOs = _postService.GetByUserKey(User.Identity.GetUserId()).OrderByDescending(post => post.CreationDate).Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _postService.Get().Count() }
            };
            return View(postViewModel);
        }
    }
}
