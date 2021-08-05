using System;
using Photo_album.DataAccess.Entities;

namespace Photo_album.DataAccess.Repositories.Abstract
{
    public interface ICommentRepository : IRepository<string, Comment>
    { }
}
