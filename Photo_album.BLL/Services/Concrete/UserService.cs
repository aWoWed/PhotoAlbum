using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Infrastructure;
using Photo_album.BLL.Services.Abstract;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.UOfW;

namespace Photo_album.BLL.Services.Concrete
{
    /// <summary>
    ///     Represents user service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Creates user service instance
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Creates new userDTO, inserts it to DB
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Operation detail</returns>
        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = _mapper.Map<UserDTO, User>(userDto);
                var result = await _unitOfWork.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                foreach (var role in userDto.Role) await _unitOfWork.UserManager.AddToRoleAsync(user.Id, role);
                await _unitOfWork.SaveAsync();

                return new OperationDetails(true, "Registration completed successfully!", "");
            }
            
            return new OperationDetails(false, "User with such Email already exists!", "Email");
        }

        /// <summary>
        ///     Authenticates user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Login user</returns>
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            var user = await _unitOfWork.UserManager.FindByNameAsync(userDto.UserName);

            if (user != null)
                claim = await _unitOfWork.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        /// <summary>
        ///     Sets data for user
        /// </summary>
        /// <param name="adminDto"></param>
        /// <param name="roles"></param>
        /// <returns>Sets data</returns>
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

        /// <summary>
        ///     Changes async user password
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>New user password</returns>
        public async Task<IdentityResult> ChangePasswordAsync(string userKey, string currentPassword,
            string newPassword) => await
            _unitOfWork.UserManager.ChangePasswordAsync(userKey, currentPassword, newPassword);

        /// <summary>
        ///     Finds user by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>User by user key</returns>
        public UserDTO FindUserByKey(string userKey)
        {
            var user = _unitOfWork.UserManager.FindById(userKey);
            return _mapper.Map<User, UserDTO>(user);
        }

        /// <summary>
        ///     Finds async user by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>User by user key</returns>
        public async Task<UserDTO> FindUserByKeyAsync(string userKey)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userKey);
            return _mapper.Map<User, UserDTO>(user);
        }

        /// <summary>
        ///     Updates user description or profile photo
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Updated user</returns>
        public async Task UpdateUserInfo(UserDTO userDto)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userDto.Id);

            user.Description = userDto.Description;
            user.ProfilePhoto = userDto.ProfilePhoto;

            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        ///     Gets all userDtos
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<UserDTO> Get() => _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_unitOfWork.UserManager.Users).AsQueryable();

        /// <summary>
        ///     Gets async all userDtos
        /// </summary>
        /// <returns>All users</returns>
        public Task<IQueryable<UserDTO>> GetAsync() => Task.FromResult(_mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_unitOfWork.UserManager.Users).AsQueryable());

        /// <summary>
        ///     Deletes userDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key)
        {
            _unitOfWork.UserManager.Delete(_unitOfWork.UserManager.FindById(key));
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Deletes Async userDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public async Task DeleteByKeyAsync(string key)
        {
            await _unitOfWork.UserManager.DeleteAsync(await _unitOfWork.UserManager.FindByIdAsync(key));
            await _unitOfWork.SaveAsync();
        }

        public void Dispose() => _unitOfWork.Dispose();
    }
}