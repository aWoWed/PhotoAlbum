using Ninject.Modules;
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
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
