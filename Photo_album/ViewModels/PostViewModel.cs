using System.Linq;
using Photo_album.BLL.DTOs;
using Photo_album.Models;

namespace Photo_album.ViewModels
{
    public class PostViewModel
    {
        public PageInfo PageInfo { get; set; }
        public IQueryable<PostDTO> PostDTOs { get; set; }
    }
}
