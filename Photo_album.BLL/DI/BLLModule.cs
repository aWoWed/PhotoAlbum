using Ninject.Modules;
using Photo_album.BLL.Services.Abstract;
using Photo_album.BLL.Services.Concrete;
using Photo_album.DataAccess.UOfW;

namespace Photo_album.BLL.DI
{
    /// <summary>
    /// Ninject module for registering needed dependencies for BLL
    /// </summary>
    public class BLLModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IPostService>().To<PostService>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
