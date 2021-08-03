using System;
using Photo_album.DataAccess.Models.Entities;

namespace Photo_album.DataAccess.Models.Repositories.Abstract
{
    public interface IPostRepository : IRepository<Guid, Post>
    { }
}
