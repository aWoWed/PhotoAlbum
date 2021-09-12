using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Photo_album.DataAccess.Context;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;
using Photo_album.DataAccess.Repositories.EntityFramework;

namespace Photo_album.DataAccess.UOfW
{
    /// <summary>
    ///     Represents unit of work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Photo_albumDbContext _appDbContext;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ILikeRepository _likeRepository;

        /// <summary>
        ///     Initializes DB context
        /// </summary>
        public UnitOfWork()
        {
            _appDbContext = new Photo_albumDbContext();
        }
        
        /// <summary>
        ///     Initializes user manager
        /// </summary>
        public UserManager<User> UserManager =>
            _userManager ??= new UserManager<User>(new UserStore<User>(_appDbContext));

        /// <summary>
        ///     Initializes role manager
        /// </summary>
        public RoleManager<IdentityRole> RoleManager =>
            _roleManager ??= new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_appDbContext));

        /// <summary>
        ///     Initializes post repository
        /// </summary>
        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_appDbContext);

        /// <summary>
        ///     Initializes comment repository
        /// </summary>
        public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(_appDbContext);
        public ILikeRepository LikeRepository => _likeRepository ??= new LikeRepository(_appDbContext);

        /// <summary>
        ///     Disposed flag
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources(DB connection, user and role managers)
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _appDbContext.Dispose();
                _userManager.Dispose();
                _roleManager.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Saves changes to DB
        /// </summary>
        public void Save() => _appDbContext.SaveChanges();

        /// <summary>
        ///     Saves changes async to DB
        /// </summary>
        public Task SaveAsync() => _appDbContext.SaveChangesAsync();
    }
}
