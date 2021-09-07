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
    /// <summary>
    ///     Represents post controller
    /// </summary>
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly ILikeService _likeService;

        /// <summary>
        ///     Creates a new instance of the <see cref="PostController" /> class
        /// </summary>
        /// <param name="postService"></param>
        /// <param name="userService"></param>
        /// <param name="commentService"></param>
        /// <param name="likeService"></param>
        public PostController(IPostService postService, IUserService userService, ICommentService commentService, ILikeService likeService)
        {
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
            _likeService = likeService;
        }

        /// <summary>
        ///     All posts with paging
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userKey"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>Posts view</returns>
        [HttpGet]
        public ActionResult Index(string searchString, string userKey, int page = 1, int pageSize = 10)
        {
            var postDtos = string.IsNullOrEmpty(userKey)
                ? _postService.Get().OrderByDescending(post => post.CreationDate).Skip((page - 1) * pageSize)
                    .Take(pageSize)
                : _postService.GetByUserKey(userKey).OrderByDescending(post => post.CreationDate)
                    .Skip((page - 1) * pageSize).Take(pageSize);
            var postViewModel = new PostViewModel
            {
                PostDTOs = string.IsNullOrEmpty(searchString) ? postDtos : postDtos.Where(post => post.Description.ToLower().Contains(searchString.ToLower())),
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
        
        /// <summary>
        ///     Current post
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Full post view</returns>
        [HttpGet]
        public ActionResult FullPost(string id)
        {
            var isLiked = _likeService.GetByUserPostKey(User.Identity.GetUserId(), id).Any();
            var fullPostViewModel = new FullPostViewModel{ IsLiked = isLiked, PostDto = _postService.GetByKey(id) };
            return View(fullPostViewModel);
        }

        /// <summary>
        ///     Creates post
        /// </summary>
        /// <returns>Post create view</returns>
        [HttpGet]
        public ActionResult PostCreate()
        {
            return View();
        }

        /// <summary>
        ///     Creates post
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Posts by current user view</returns>
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
                var postDto = new PostDTO
                {
                    Description = post.Description.Trim(),
                    Image = base64,
                    UserId = User.Identity.GetUserId()
                };
                await _postService.InsertAsync(postDto);
                return RedirectToAction("Index", new { userKey = postDto.UserId });
            }

            return View("PostCreate");
        }
        
        /// <summary>
        ///     Creates comment in current post
        /// </summary>
        /// <param name="postKey"></param>
        /// <param name="text"></param>
        /// <returns>Full post view with new comment</returns>
        [HttpPost]
        public async Task<ActionResult> AddComment(string postKey, string text)
        {
            if (string.IsNullOrEmpty(text))
                ModelState.AddModelError("Text", "Error! Please enter your text!");

            var userId = User.Identity.GetUserId();

            var isLiked = _likeService.GetByUserPostKey(userId, postKey).Any();
            var fullPostViewModel = new FullPostViewModel { IsLiked = isLiked, PostDto = _postService.GetByKey(postKey) };

            if (ModelState.IsValid)
            {
                var post = _postService.GetByKey(postKey);

                post.CommentDtos.Add(new CommentDTO
                {
                    Text = text,
                    PostId = postKey,
                    UserId = userId,
                    UserDto = _userService.FindUserByKey(User.Identity.GetUserId()),
                    PostDto = post,
                });

                await _postService.UpdateAsync(post);

                await _commentService.InsertAsync(new CommentDTO
                {
                    Text = text,
                    PostId = postKey,
                    UserId = userId,
                });

                isLiked = _likeService.GetByUserPostKey(userId, postKey).Any();
                fullPostViewModel = new FullPostViewModel { IsLiked = isLiked, PostDto = post };

                ModelState.Clear();

                return View("FullPost", fullPostViewModel);
            }

            return View("FullPost", fullPostViewModel);
        }

        /// <summary>
        ///     Edits comment info
        /// </summary>
        /// <param name="commentKey"></param>
        /// <returns>Edit comment view</returns>
        [HttpGet]
        public ActionResult EditComment(string commentKey)
        {
            return View(_commentService.GetByKey(commentKey));
        }

        /// <summary>
        ///     Edits comment info
        /// </summary>
        /// <param name="commentKey"></param>
        /// <param name="text"></param>
        /// <returns>Full post view</returns>
        [HttpPost]
        public async Task<ActionResult> EditComment(string commentKey, string text)
        {
            if (string.IsNullOrEmpty(text))
                ModelState.AddModelError("Text", "Error! Please enter your text!");

            if (ModelState.IsValid)
            {
                var comment = await _commentService.GetByKeyAsync(commentKey);
                comment.Text = text;
                await _commentService.UpdateAsync(comment);
                return RedirectToAction("FullPost", new { id = comment.PostId });
            }

            return View("EditComment", await _commentService.GetByKeyAsync(commentKey));
        }

        /// <summary>
        ///     Deletes comment
        /// </summary>
        /// <param name="postKey"></param>
        /// <param name="commentKey"></param>
        /// <returns>Full post view without this comment</returns>
        [HttpGet]
        public async Task<ActionResult> DeleteComment(string postKey, string commentKey)
        {
            await _commentService.DeleteByKeyAsync(commentKey);
            return RedirectToAction("FullPost", new {id = postKey});
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
            return RedirectToAction("FullPost", new { id = postKey });
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

        /// <summary>
        ///     Edits post info
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>Edit post view</returns>
        [HttpGet]
        public ActionResult EditPost(string postKey)
        {
            var editPostViewModel = new EditPostViewModel
            {
                PostDto = _postService.GetByKey(postKey),
                PostCreate = new PostCreate()
            };
            return View(editPostViewModel);
        }

        /// <summary>
        ///     Edits post info
        /// </summary>
        /// <param name="post"></param>
        /// <param name="postKey"></param>
        /// <returns>Posts by current user view</returns>
        [HttpPost]
        public async Task<ActionResult> EditPost(PostCreate post, string postKey)
        {
            if (string.IsNullOrEmpty(post.Description))
                ModelState.AddModelError("Description", "Error! Please enter your description!");

            if (ModelState.IsValid)
            {
                var postDto = _postService.GetByKey(postKey);
                if (post.Image != null)
                {
                    var bytes = new byte[post.Image.ContentLength];
                    await post.Image.InputStream.ReadAsync(bytes, 0, bytes.Length);
                    var base64 = Convert.ToBase64String(bytes);
                    postDto.Image = base64;
                }
                postDto.Description = post.Description;
                await _postService.UpdateAsync(postDto);
                return RedirectToAction("Index", new {userKey = postDto.UserId});
            }

            return View("EditPost");
        }

        /// <summary>
        ///     Deletes post
        /// </summary>
        /// <param name="postKey"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns>Posts by current user view without this post</returns>
        [HttpGet]
        public async Task<ActionResult> DeletePost(string postKey, int page = 1, int pageSize = 10)
        {
            await _postService.DeleteByKeyAsync(postKey);
            var postViewModel = new PostViewModel
            {
                PostDTOs = _postService.GetByUserKey(User.Identity.GetUserId()).OrderByDescending(post => post.CreationDate).Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _postService.Get().Count() }
            };
            return RedirectToAction("Index", postViewModel);
        }
    }
}