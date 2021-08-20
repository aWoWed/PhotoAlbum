using System;
using Photo_album.DataAccess.Entities;

namespace Photo_album.DataAccess.Repositories.Abstract
{
    public interface IPostRepository : IRepository<string, Post>
    {
        /// <summary>
        /// Counts posts, which user with userKey made
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns>Number of posts made by user with userKey</returns>
        int Count(string userKey);
    }
}
