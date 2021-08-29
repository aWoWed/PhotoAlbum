using System;
using System.Diagnostics;
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

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var postViewModel = new PostViewModel {PostDTOs = _postService.Get()};
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
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
