using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;

namespace Photo_album.DataAccess.UOfW
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }
        ICommentRepository CommentRepository { get; }
        IPostRepository PostRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
