using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photo_album.Models.Repositories.Abstract
{
    public interface IRepository<in TEntityKey, TEntity>
    {
        IEnumerable<TEntity> Get();
        Task<IEnumerable<TEntity>> GetAsync();

        TEntity GetByKey(TEntityKey key);
        Task<TEntity> GetByKeyAsync(TEntityKey key);

        bool Add(TEntity asset);
        Task<bool> AddAsync(TEntity Asset);

        bool Update(TEntity asset);
        Task<bool> UpdateAsync(TEntity asset);

        bool DeleteByKey(TEntityKey index);
        Task<bool> DeleteByKeyAsync(TEntityKey key);

        bool DeleteAll();
        Task<bool> DeleteAllAsync();
    }
}
