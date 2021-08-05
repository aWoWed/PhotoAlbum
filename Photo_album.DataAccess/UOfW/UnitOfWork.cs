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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Photo_albumDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public UnitOfWork(Photo_albumDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public UserManager<User> UserManager => _userManager ?? new UserManager<User>(new UserStore<User>(_appDbContext));
        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_appDbContext);
        public ICommentRepository CommentRepository => _commentRepository ?? new CommentRepository(_appDbContext);

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                }

                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save() => _appDbContext.SaveChanges();
        public async Task SaveAsync() => await _appDbContext.SaveChangesAsync();
    }
}
