using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photo_album.Models.Entities;
using Photo_album.Models.Repositories.Abstract;

namespace Photo_album.Models.Repositories.EntityFramework
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        public CommentRepository(Photo_albumDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Comment> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Comment GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool Add(Comment asset)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(Comment Asset)
        {
            throw new NotImplementedException();
        }

        public bool Update(Comment asset)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Comment asset)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(string index)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
