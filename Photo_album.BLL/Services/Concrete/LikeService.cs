using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Services.Abstract;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.UOfW;

namespace Photo_album.BLL.Services.Concrete
{
    /// <summary>
    ///     Represents like service
    /// </summary>
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Creates like service instance
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Gets All likeDTOs
        /// </summary>
        /// <returns>LikeDTOs from Db</returns>
        public IQueryable<LikeDTO> Get() => 
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(_unitOfWork.LikeRepository.Get())
                .AsQueryable();

        /// <summary>
        ///     Gets Async All likeDTOs
        /// </summary>
        /// <returns>LikeDTOs from Db</returns>
        public async Task<IQueryable<LikeDTO>> GetAsync() => 
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(await _unitOfWork.LikeRepository.GetAsync())
                .AsQueryable();

        /// <summary>
        ///     Gets likeDTO by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>LikeDTO by key</returns>
        public LikeDTO GetByKey(string key) =>
            _mapper.Map<Like, LikeDTO>(_unitOfWork.LikeRepository.GetByKey(key));

        /// <summary>
        ///     Gets Async likeDTO by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>LikeDTO by key</returns>
        public async Task<LikeDTO> GetByKeyAsync(string key) =>
            _mapper.Map<Like, LikeDTO>(await _unitOfWork.LikeRepository.GetByKeyAsync(key));

        /// <summary>
        ///     Gets likeDTOs by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>LikeDTOs by user key</returns>
        public IQueryable<LikeDTO> GetByUserKey(string userKey) =>
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(_unitOfWork.LikeRepository.GetByUserKey(userKey))
                .AsQueryable();

        /// <summary>
        ///     Gets Async likeDTOs by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>LikeDTOs by user key</returns>
        public async Task<IQueryable<LikeDTO>> GetByUserKeyAsync(string userKey) =>
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(await _unitOfWork.LikeRepository.GetByUserKeyAsync(userKey))
                .AsQueryable();

        /// <summary>
        ///     Inserts likeDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public void Insert(LikeDTO entity)
        {
            var like = _mapper.Map<LikeDTO, Like>(entity);
            _unitOfWork.LikeRepository.Insert(like);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Inserts Async likeDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public async Task<LikeDTO> InsertAsync(LikeDTO entity)
        {
            var like = _mapper.Map<LikeDTO, Like>(entity);
            _unitOfWork.LikeRepository.Insert(like);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        /// <summary>
        ///     Updates likeDTO to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Update(LikeDTO entity)
        {
            var like = _unitOfWork.LikeRepository.GetByKey(entity.Id);
            _unitOfWork.LikeRepository.Update(like);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Updates Async likeDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public async Task<LikeDTO> UpdateAsync(LikeDTO entity)
        {
            var like = await _unitOfWork.LikeRepository.GetByKeyAsync(entity.Id);
            _unitOfWork.LikeRepository.Update(like);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        /// <summary>
        ///     Deletes likeDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key)
        {
            _unitOfWork.PostRepository.DeleteByKey(key);
            _unitOfWork.SaveAsync();
        }

        /// <summary>
        ///     Deletes Async likeDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public async Task DeleteByKeyAsync(string key)
        {
            await _unitOfWork.LikeRepository.DeleteByKeyAsync(key);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        ///     Deletes likeDTOs
        /// </summary>
        public void DeleteAll() => _unitOfWork.LikeRepository.DeleteAll();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose() => _unitOfWork.Dispose();

        /// <summary>
        ///     Gets All likeDTOs by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>LikeDTOs with current post key from Db</returns>
        public IQueryable<LikeDTO> GetByPostKey(string postKey) =>
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(_unitOfWork.LikeRepository.GetByPostKey(postKey))
                .AsQueryable();

        /// <summary>
        ///     Gets Async All likeDTOs by post key from Db
        /// </summary>
        /// <param name="postKey"></param>
        /// <returns>LikeDTOs with current post key from Db</returns>
        public async Task<IQueryable<LikeDTO>> GetByPostKeyAsync(string postKey) =>
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(await _unitOfWork.LikeRepository.GetByPostKeyAsync(postKey))
                .AsQueryable();

        public IQueryable<LikeDTO> GetByUserPostKey(string userKey, string postKey) => 
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(_unitOfWork.LikeRepository.GetByUserPostKey(userKey, postKey))
                .AsQueryable();

        public async Task<IQueryable<LikeDTO>> GetByUserPostKeyAsync(string userKey, string postKey) =>
            _mapper.Map<IEnumerable<Like>, IEnumerable<LikeDTO>>(await _unitOfWork.LikeRepository.GetByUserPostKeyAsync(userKey, postKey))
                .AsQueryable();
    }
}
