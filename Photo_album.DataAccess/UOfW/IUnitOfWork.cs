using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;

namespace Photo_album.DataAccess.UOfW
{
    /// <summary>
    ///     Provides interface for unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Gets role manager
        /// </summary>
        RoleManager<IdentityRole> RoleManager { get; }

        /// <summary>
        ///     Gets user manager
        /// </summary>
        UserManager<User> UserManager { get; }

        /// <summary>
        ///     Gets comment repository
        /// </summary>
        ICommentRepository CommentRepository { get; }

        /// <summary>
        ///     Gets post repository
        /// </summary>
        IPostRepository PostRepository { get; }

        /// <summary>
        ///     Gets like repository
        /// </summary>
        ILikeRepository LikeRepository { get; }

        /// <summary>
        ///     Saves changes to DB
        /// </summary>
        void Save();

        /// <summary>
        ///     Saves changes async to DB
        /// </summary>
        Task SaveAsync();
    }
}
