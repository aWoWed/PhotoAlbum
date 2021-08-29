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
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        IQueryable<PostDTO> IService<string, PostDTO>.Get() =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(_unitOfWork.PostRepository.Get()).AsQueryable();

        public async Task<IQueryable<PostDTO>> GetAsync() =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(await _unitOfWork.PostRepository.GetAsync())
                .AsQueryable();    

        public PostDTO GetByKey(string key) => 
            _mapper.Map<Post, PostDTO>(_unitOfWork.PostRepository.GetByKey(key));    


        public async Task<PostDTO> GetByKeyAsync(string key) => 
                _mapper.Map<Post, PostDTO>( await _unitOfWork.PostRepository.GetByKeyAsync(key));

        public IQueryable<PostDTO> GetByUserKey(string userKey) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(_unitOfWork.PostRepository.GetByUserKey(userKey))
                .AsQueryable();

        public async Task<IQueryable<PostDTO>> GetByUserKeyAsync(string userKey) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(
                await _unitOfWork.PostRepository.GetByUserKeyAsync(userKey)).AsQueryable();

        public IQueryable<PostDTO> GetByContainsText(string text) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(_unitOfWork.PostRepository.GetByContainsText(text))
                .AsQueryable();

        public async Task<IQueryable<PostDTO>> GetByContainsTextAsync(string text) =>
            _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(
                await _unitOfWork.PostRepository.GetByContainsTextAsync(text)).AsQueryable();
        
        public void Insert(PostDTO entity)
        {
            _unitOfWork.PostRepository.Insert(_mapper.Map<PostDTO, Post>(entity));
            _unitOfWork.Save();
        }

        public async Task<PostDTO> InsertAsync(PostDTO entity)
        {
            var post = _mapper.Map<PostDTO, Post>(entity);
            _unitOfWork.PostRepository.Insert(post);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public void Update(PostDTO entity)
        {
            _unitOfWork.PostRepository.Update(_mapper.Map<PostDTO, Post>(entity));
            _unitOfWork.Save();
        }

        public async Task<PostDTO> UpdateAsync(PostDTO entity)
        {
            var post = _mapper.Map<PostDTO, Post>(entity);
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public void DeleteByKey(string key)
        {
            _unitOfWork.PostRepository.DeleteByKey(key);
            _unitOfWork.Save();
        }

        public async Task DeleteByKeyAsync(string key)
        {
            await _unitOfWork.PostRepository.DeleteByKeyAsync(key);
            await _unitOfWork.SaveAsync();
        }

        public void DeleteAll() => _unitOfWork.PostRepository.DeleteAll();

        public void Dispose() => _unitOfWork.Dispose();
    }
}