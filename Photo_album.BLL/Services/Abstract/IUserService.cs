using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Infrastructure;

namespace Photo_album.BLL.Services.Abstract
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);

        Task<IdentityResult> ChangePasswordAsync(string userKey, string currentPassword, string newPassword);
        UserDTO FindUserByKey(string userKey);

        Task<UserDTO> FindUserByKeyAsync(string userKey);
        Task UpdateUserInfo(UserDTO userDto);
    }
}