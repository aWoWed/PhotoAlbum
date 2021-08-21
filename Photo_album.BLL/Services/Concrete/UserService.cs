using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Infrastructure;
using Photo_album.BLL.Services.Abstract;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.UOfW;

namespace Photo_album.BLL.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email };
                var result = await _unitOfWork.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                foreach (var role in userDto.Role) await _unitOfWork.UserManager.AddToRoleAsync(user.Id, role);
                await _unitOfWork.SaveAsync();

                return new OperationDetails(true, "Registration completed successfully!", "");
            }
            
            return new OperationDetails(false, "User with such Email already exists!", "Email");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            var user = await _unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
                claim = await _unitOfWork.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await _unitOfWork.RoleManager.FindByNameAsync(roleName);

                if (role == null)
                {
                    role = new IdentityRole { Name = roleName };
                    await _unitOfWork.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userKey, string currentPassword,
            string newPassword) => await
            _unitOfWork.UserManager.ChangePasswordAsync(userKey, currentPassword, newPassword);

        public async Task<UserDTO> FindUserByKeyAsync(string userKey)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userKey);
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Email,
                Password = user.PasswordHash,
            };
        }

        public void Dispose() => _unitOfWork.Dispose();
    }
}