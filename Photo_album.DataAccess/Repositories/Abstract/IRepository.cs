using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photo_album.DataAccess.Repositories.Abstract
{
    public interface IRepository<in TEntityKey, TEntity>
    {
        IQueryable<TEntity> Get();
        Task<IQueryable<TEntity>> GetAsync();

        TEntity GetByKey(TEntityKey key);
        Task<TEntity> GetByKeyAsync(TEntityKey key);

        IQueryable<TEntity> GetByUserKey(TEntityKey userKey);
        Task<IQueryable<TEntity>> GetByUserKeyAsync(TEntityKey userKey);

        IQueryable<TEntity> GetByContainsText(string text);
        Task<IQueryable<TEntity>> GetByContainsTextAsync(string text);

        void Save(TEntity entity);

        void DeleteByKey(TEntityKey key);

        void DeleteAll();
    }
}
