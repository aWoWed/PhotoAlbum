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
    ///     Represents post service
    /// </summary>
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Creates post service instance
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Gets All postDTOs
        /// </summary>
        /// <returns>postDTOs from Db</returns>
        public IQueryable<PostDTO> Get() => _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(_unitOfWork.PostRepository.Get()).AsQueryable();

        /// <summary>
        ///     Gets Async All postDTOs
        /// </summary>
        /// <returns>postDTOs from Db</returns>
        public async Task<IQueryable<PostDTO>> GetAsync() =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(await _unitOfWork.PostRepository.GetAsync())
                .AsQueryable();

        /// <summary>
        ///     Gets postDTO by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>postDTO by key</returns>
        public PostDTO GetByKey(string key) => 
            _mapper.Map<Post, PostDTO>(_unitOfWork.PostRepository.GetByKey(key));

        /// <summary>
        ///     Gets Async postDTO by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>postDTO by key</returns>
        public async Task<PostDTO> GetByKeyAsync(string key) => 
                _mapper.Map<Post, PostDTO>( await _unitOfWork.PostRepository.GetByKeyAsync(key));

        /// <summary>
        ///     Gets postDTOs by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>postDTOs by user key</returns>
        public IQueryable<PostDTO> GetByUserKey(string userKey) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(_unitOfWork.PostRepository.GetByUserKey(userKey))
                .AsQueryable();

        /// <summary>
        ///     Gets Async postDTOs by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>postDTOs by user key</returns>
        public async Task<IQueryable<PostDTO>> GetByUserKeyAsync(string userKey) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(
                await _unitOfWork.PostRepository.GetByUserKeyAsync(userKey)).AsQueryable();

        /// <summary>
        ///     Gets All postDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All postDTOs, which contain current text, from Db</returns>
        public IQueryable<PostDTO> GetByContainsText(string text) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(_unitOfWork.PostRepository.GetByContainsText(text))
                .AsQueryable();

        /// <summary>
        ///     Gets Async All postDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns> All postDTOs, which contain current text, from Db</returns>
        public async Task<IQueryable<PostDTO>> GetByContainsTextAsync(string text) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(
                await _unitOfWork.PostRepository.GetByContainsTextAsync(text)).AsQueryable();

        /// <summary>
        ///     Inserts postDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public void Insert(PostDTO entity)
        {
            _unitOfWork.PostRepository.Insert(_mapper.Map<PostDTO, Post>(entity));
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Inserts Async postDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public async Task<PostDTO> InsertAsync(PostDTO entity)
        {
            var post = _mapper.Map<PostDTO, Post>(entity);
            _unitOfWork.PostRepository.Insert(post);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        /// <summary>
        ///     Updates postDTO to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Update(PostDTO entity)
        {
            _unitOfWork.PostRepository.Update(_mapper.Map<PostDTO, Post>(entity));
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Updates Async postDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public async Task<PostDTO> UpdateAsync(PostDTO entity)
        {
            _unitOfWork.PostRepository.Update(_mapper.Map<PostDTO, Post>(entity));
            await _unitOfWork.SaveAsync();
            return entity;
        }

        /// <summary>
        ///     Deletes postDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key)
        {
            _unitOfWork.CommentRepository.DeleteByPostKey(key);
            _unitOfWork.PostRepository.DeleteByKey(key);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Deletes Async postDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public async Task DeleteByKeyAsync(string key)
        {
            await _unitOfWork.CommentRepository.DeleteByPostKeyAsync(key);
            await _unitOfWork.PostRepository.DeleteByKeyAsync(key);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        ///     Deletes postDTOs
        /// </summary>
        public void DeleteAll() => _unitOfWork.PostRepository.DeleteAll();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose() => _unitOfWork.Dispose();
    }
}