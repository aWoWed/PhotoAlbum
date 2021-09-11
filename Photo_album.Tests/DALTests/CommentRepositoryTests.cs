using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photo_album.DataAccess.Context;

namespace Photo_album.Tests.DALTests
{
    public class CommentRepositoryTests
    {
        public static DbContextOptions<Photo_albumDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<Photo_albumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new Photo_albumDbContext(options))
            {
                SeedData(context);
            }
            return options;
        }

    }
}
