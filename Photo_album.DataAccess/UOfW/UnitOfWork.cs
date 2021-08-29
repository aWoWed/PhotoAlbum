using System;
using System.Data.Entity.Validation;
using System.Linq;
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
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;

        public UnitOfWork()
        {
            _appDbContext = new Photo_albumDbContext();
        }

        public UserManager<User> UserManager =>
            _userManager ??= new UserManager<User>(new UserStore<User>(_appDbContext));

        public RoleManager<IdentityRole> RoleManager =>
            _roleManager ??= new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_appDbContext));
        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_appDbContext);
        public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(_appDbContext);

        private bool _disposed;

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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save() => _appDbContext.SaveChanges();
        public Task SaveAsync() => _appDbContext.SaveChangesAsync();
    }
}
