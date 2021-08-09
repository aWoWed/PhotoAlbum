﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Photo_album.DataAccess.Context;
using Photo_album.DataAccess.Entities;
using Photo_album.DataAccess.Repositories.Abstract;

namespace Photo_album.DataAccess.Repositories.EntityFramework
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Photo_albumDbContext _appDbContext;

        public CommentRepository(Photo_albumDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<Comment> Get() => _appDbContext.Comments;

        public Task<IQueryable<Comment>> GetAsync() => Task.FromResult(_appDbContext.Comments.AsQueryable());

        public Comment GetByKey(string key) =>
            _appDbContext.Comments.FirstOrDefault(comment => comment.Id == key);

        public async Task<Comment> GetByKeyAsync(string key) => await _appDbContext.Comments.FirstOrDefaultAsync(comment => comment.Id == key);

        public IQueryable<Comment> GetByUserKey(string userKey) =>
            _appDbContext.Comments.Where(comment => comment.UserId == userKey);

        public Task<IQueryable<Comment>> GetByUserKeyAsync(string userKey) =>
            Task.FromResult(_appDbContext.Comments.Where(comment => comment.UserId == userKey));

        public IQueryable<Comment> GetByContainsText(string text) =>
            _appDbContext.Comments.Where(comment => comment.Text.ToLower().Contains(text.ToLower()));

        public Task<IQueryable<Comment>> GetByContainsTextAsync(string text) =>
            Task.FromResult(_appDbContext.Comments.Where(comment => comment.Text.ToLower().Contains(text.ToLower())));

        public void Save(Comment entity) => _appDbContext.Entry(entity).State =
            _appDbContext.Comments.FirstOrDefault(comment => comment.Id == entity.Id) == null
                ? EntityState.Added
                : EntityState.Modified;
        
        public void DeleteByKey(string key) => _appDbContext.Comments.Remove(new Comment {Id = key});
        
        public void DeleteAll() => _appDbContext.Comments.RemoveRange(_appDbContext.Comments);
    }
}
