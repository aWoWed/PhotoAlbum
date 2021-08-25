using System.Linq;
using System.Threading.Tasks;

namespace Photo_album.DataAccess.Repositories.Abstract
{
    public interface IRepository<in TEntityKey, TEntity>
    {
        /// <summary>
        /// Gets All entity elems from Db
        /// </summary>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Gets Async All entity elems from Db
        /// </summary>
        /// <returns>Elems from Db</returns>
        Task<IQueryable<TEntity>> GetAsync();

        /// <summary>
        /// Gets an entity elem by key from Db
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Entity elem from Db</returns>
        TEntity GetByKey(TEntityKey key);

        /// <summary>
        /// Gets Async an entity elem by key from Db
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Entity elem from Db</returns>
        Task<TEntity> GetByKeyAsync(TEntityKey key);

        /// <summary>
        /// Gets All entity elems by userKey from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Entity elems with current userKey from Db</returns>
        IQueryable<TEntity> GetByUserKey(TEntityKey userKey);

        /// <summary>
        /// Gets Async All entity elems by userKey from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Entity elems with current userKey from Db</returns>
        Task<IQueryable<TEntity>> GetByUserKeyAsync(TEntityKey userKey);

        /// <summary>
        /// Gets All entity elems, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns> All entity elems, which contain current text, from Db</returns>
        IQueryable<TEntity> GetByContainsText(string text);

        /// <summary>
        /// Gets Async All entity elems, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns> All entity elems, which contain current text, from Db</returns>
        Task<IQueryable<TEntity>> GetByContainsTextAsync(string text);

        /// <summary>
        /// Inserts new entity to Db
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates entity from Db
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes entity by current key
        /// </summary>
        /// <param name="key"></param>
        void DeleteByKey(TEntityKey key);
        
        /// <summary>
        /// Deletes Async entity by current key
        /// </summary>
        /// <param name="key"></param>
        Task DeleteByKeyAsync(TEntityKey key);

        /// <summary>
        /// Deletes all entity elems from Db
        /// </summary>
        void DeleteAll();
    }
}
