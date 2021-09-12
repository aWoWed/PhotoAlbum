using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Infrastructure;

namespace Photo_album.BLL.Services.Abstract
{
    /// <summary>
    ///     Provides interface for user service
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        ///     Creates new userDTO, inserts it to DB
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Operation detail</returns>
        Task<OperationDetails> Create(UserDTO userDto);

        /// <summary>
        ///     Authenticates user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Login user</returns>
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);

        /// <summary>
        ///     Sets data for user
        /// </summary>
        /// <param name="adminDto"></param>
        /// <param name="roles"></param>
        /// <returns>Sets data</returns>
        Task SetInitialData(UserDTO adminDto, List<string> roles);

        /// <summary>
        ///     Changes async user password
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>New user password</returns>
        Task<IdentityResult> ChangePasswordAsync(string userKey, string currentPassword, string newPassword);

        /// <summary>
        ///     Gets all userDTOs
        /// </summary>
        /// <returns>All users</returns>
        IQueryable<UserDTO> Get();

        /// <summary>
        ///     Gets Async all userDTOs
        /// </summary>
        /// <returns>All users</returns>
        Task<IQueryable<UserDTO>> GetAsync();

        /// <summary>
        ///     Finds user by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>User by user key</returns>
        UserDTO FindUserByKey(string userKey);

        /// <summary>
        ///     Finds async user by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>User by user key</returns>
        Task<UserDTO> FindUserByKeyAsync(string userKey);

        /// <summary>
        ///     Updates user description or profile photo
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Updated user</returns>
        Task UpdateUserInfo(UserDTO userDto);

        /// <summary>
        ///     Deletes userDTO by Key
        /// </summary>
        /// <param name="key"></param>
        void DeleteByKey(string key);

        /// <summary>
        ///     Deletes Async userDTO by Key
        /// </summary>
        /// <param name="key"></param>
        Task DeleteByKeyAsync(string key);
    }
}