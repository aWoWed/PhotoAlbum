﻿using System;
using Photo_album.DataAccess.Models.Entities;

namespace Photo_album.DataAccess.Models.Repositories.Abstract
{
    public interface ICommentRepository : IRepository<Guid, Comment>
    { }
}
