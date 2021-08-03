using System;
using Photo_album.Models.Entities;

namespace Photo_album.Models.Repositories.Abstract
{
    public interface ICommentRepository : IRepository<Guid, Comment>
    { }
}
