using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Photo_album.BLL.DTOs;
using Photo_album.BLL.Services.Abstract;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.UOfW;

namespace Photo_album.BLL.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<CommentDTO> Get() =>
            _mapper.Map<IQueryable<Comment>, IQueryable<CommentDTO>>(_unitOfWork.CommentRepository.Get());

        public async Task<IQueryable<CommentDTO>> GetAsync() =>
            _mapper.Map<IQueryable<Comment>, IQueryable<CommentDTO>>(await _unitOfWork.CommentRepository.GetAsync());

        public CommentDTO GetByKey(string key) =>
            _mapper.Map<Comment, CommentDTO>(_unitOfWork.CommentRepository.GetByKey(key));

        public async Task<CommentDTO> GetByKeyAsync(string key) =>
            _mapper.Map<Comment, CommentDTO>(await _unitOfWork.CommentRepository.GetByKeyAsync(key));

        public IQueryable<CommentDTO> GetByUserKey(string userKey) =>
            _mapper.Map<IQueryable<Comment>, IQueryable<CommentDTO>>(_unitOfWork.CommentRepository.GetByUserKey(userKey));

        public async Task<IQueryable<CommentDTO>> GetByUserKeyAsync(string userKey) =>
            _mapper.Map<IQueryable<Comment>, IQueryable<CommentDTO>>(await _unitOfWork.CommentRepository.GetByUserKeyAsync(userKey));

        public IQueryable<CommentDTO> GetByContainsText(string text) =>
            _mapper.Map<IQueryable<Comment>, IQueryable<CommentDTO>>(_unitOfWork.CommentRepository.GetByContainsText(text));

        public async Task<IQueryable<CommentDTO>> GetByContainsTextAsync(string text) =>
        _mapper.Map<IQueryable<Comment>, IQueryable<CommentDTO>>(await _unitOfWork.CommentRepository.GetByContainsTextAsync(text)); 
        
        public void Insert(CommentDTO entity)
        {
            var comment = _mapper.Map<CommentDTO, Comment>(entity);
            _unitOfWork.CommentRepository.Insert(comment);
            _unitOfWork.Save();
        }

        public async Task<CommentDTO> InsertAsync(CommentDTO entity)
        {
            var comment = _mapper.Map<CommentDTO, Comment>(entity);
            _unitOfWork.CommentRepository.Insert(comment);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public void Update(CommentDTO entity)
        {
            var comment = _mapper.Map<CommentDTO, Comment>(entity);
            _unitOfWork.CommentRepository.Update(comment);
            _unitOfWork.Save();
        }

        public async Task<CommentDTO> UpdateAsync(CommentDTO entity)
        {
            var comment = _mapper.Map<CommentDTO, Comment>(entity);
            _unitOfWork.CommentRepository.Update(comment);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public void DeleteByKey(string key)
        {
            _unitOfWork.CommentRepository.DeleteByKey(key);
            _unitOfWork.Save();
        }

        public async Task DeleteByKeyAsync(string key)
        {
            await _unitOfWork.CommentRepository.DeleteByKeyAsync(key);
            await _unitOfWork.SaveAsync();
        }

        public void DeleteAll() => _unitOfWork.CommentRepository.DeleteAll();

        public void Dispose() => _unitOfWork.Dispose();
    }
}