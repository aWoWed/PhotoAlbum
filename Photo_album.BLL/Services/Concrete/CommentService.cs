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
    ///     Represents comment service
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        ///     Creates comment service instance
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Gets All commentDTOs
        /// </summary>
        /// <returns>CommentDTOs from Db</returns>
        public IQueryable<CommentDTO> Get() =>
            _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(_unitOfWork.CommentRepository.Get())
                .AsQueryable();

        /// <summary>
        ///     Gets Async All commentDTOs
        /// </summary>
        /// <returns>CommentDTOs from Db</returns>
        public async Task<IQueryable<CommentDTO>> GetAsync() =>
            _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(await _unitOfWork.CommentRepository.GetAsync())
                .AsQueryable();

        /// <summary>
        ///     Gets commentDTO by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>CommentDTO by key</returns>
        public CommentDTO GetByKey(string key) =>
            _mapper.Map<Comment, CommentDTO>(_unitOfWork.CommentRepository.GetByKey(key));


        /// <summary>
        ///     Gets Async commentDTO by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>CommentDTO by key</returns>
        public async Task<CommentDTO> GetByKeyAsync(string key) =>
            _mapper.Map<Comment, CommentDTO>(await _unitOfWork.CommentRepository.GetByKeyAsync(key));

        /// <summary>
        ///     Gets commentDTOs by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>CommentDTOs by user key</returns>
        public IQueryable<CommentDTO> GetByUserKey(string userKey) =>
            _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(
                _unitOfWork.CommentRepository.GetByUserKey(userKey)).AsQueryable();

        /// <summary>
        ///     Gets Async commentDTOs by user key
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>CommentDTOs by user key</returns>
        public async Task<IQueryable<CommentDTO>> GetByUserKeyAsync(string userKey) =>
            _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(
                await _unitOfWork.CommentRepository.GetByUserKeyAsync(userKey)).AsQueryable();

        /// <summary>
        ///     Gets All commentDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns>All commentDTOs, which contain current text, from Db</returns>
        public IQueryable<CommentDTO> GetByContainsText(string text) =>
            _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(
                _unitOfWork.CommentRepository.GetByContainsText(text)).AsQueryable();

        /// <summary>
        ///     Gets Async All commentDTOs, which contain current text in Text field from Db
        /// </summary>
        /// <param name="text"></param>
        /// <returns> All commentDTOs, which contain current text, from Db</returns>
        public async Task<IQueryable<CommentDTO>> GetByContainsTextAsync(string text) =>
            _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(
                await _unitOfWork.CommentRepository.GetByContainsTextAsync(text)).AsQueryable();

        /// <summary>
        ///     Inserts commentDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public void Insert(CommentDTO entity)
        {
            var comment = _mapper.Map<CommentDTO, Comment>(entity);
            _unitOfWork.CommentRepository.Insert(comment);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Inserts Async commentDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public async Task<CommentDTO> InsertAsync(CommentDTO entity)
        {
            var comment = _mapper.Map<CommentDTO, Comment>(entity);
            _unitOfWork.CommentRepository.Insert(comment);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        /// <summary>
        ///     Updates commentDTO to Db
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CommentDTO entity)
        {
            var comment = _unitOfWork.CommentRepository.GetByKey(entity.Id);
            comment.Text = entity.Text;
            _unitOfWork.CommentRepository.Update(comment);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Updates Async commentDTO to Db
        /// </summary>s
        /// <param name="entity"></param>
        public async Task<CommentDTO> UpdateAsync(CommentDTO entity)
        {
            var comment = await _unitOfWork.CommentRepository.GetByKeyAsync(entity.Id);
            comment.Text = entity.Text;
            _unitOfWork.CommentRepository.Update(comment);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        /// <summary>
        ///     Deletes commentDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public void DeleteByKey(string key)
        {
            _unitOfWork.CommentRepository.DeleteByKey(key);
            _unitOfWork.Save();
        }

        /// <summary>
        ///     Deletes Async commentDTO by Key
        /// </summary>
        /// <param name="key"></param>
        public async Task DeleteByKeyAsync(string key)
        {
            await _unitOfWork.CommentRepository.DeleteByKeyAsync(key);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        ///     Deletes commentDTOs
        /// </summary>
        public void DeleteAll() => _unitOfWork.CommentRepository.DeleteAll();

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose() => _unitOfWork.Dispose();
    }
}