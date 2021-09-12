using System;
using System.Linq;
using System.Threading.Tasks;

namespace Photo_album.BLL.Services.Abstract
{
    /// <summary>
    ///     Provides base interface for other interfaces
    /// </summary>
    /// <typeparam name="TEntityKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<in TEntityKey, TEntity> : IDisposable
    {
        /// <summary>
        ///     Gets All DTO elems
        /// </summary>
        /// <returns>DTO Elems from Db</returns>
        IQueryable<TEntity> Get();

        /// <summary>
        ///      Gets Async All DTO elems
        /// </summary>
        /// <returns>DTO Elems from Db</returns>
        Task<IQueryable<TEntity>> GetAsync();

        /// <summary>
        ///     Gets elem by Key from DTO
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Elem by key from Db</returns>
        TEntity GetByKey(TEntityKey key);

        /// <summary>
        ///     Gets Async elem by Key from DTO
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Elem by key from Db</returns>
        Task<TEntity> GetByKeyAsync(TEntityKey key);

        /// <summary>
        ///     Gets All entity elems by userKey from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Entity elems with current userKey from Db</returns>
        IQueryable<TEntity> GetByUserKey(TEntityKey userKey);

        /// <summary>
        ///     Gets Async All entity elems by userKey from Db
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Entity elems with current userKey from Db</returns>
        Task<IQueryable<TEntity>> GetByUserKeyAsync(TEntityKey userKey);

        /// <summary>
        ///     Inserts elem to Db
        /// </summary>s
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        
        /// <summary>
        ///     Inserts Async elem to Db
        /// </summary>s
        /// <param name="entity"></param>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        ///     Updates elem to Db
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        
        /// <summary>
        ///     Updates Async elem to Db
        /// </summary>
        /// <param name="entity"></param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        ///     Deletes element by Key
        /// </summary>
        /// <param name="key"></param>
        void DeleteByKey(TEntityKey key);

        /// <summary>
        ///     Deletes Async element by Key
        /// </summary>
        /// <param name="key"></param>
        Task DeleteByKeyAsync(TEntityKey key);
        
        /// <summary>
        ///     Deletes All elems
        /// </summary>
        void DeleteAll();
    }

}