using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Photo_album.DataAccess.Entities;

namespace Photo_album.DataAccess.Context
{
    public class Photo_albumDbContext : IdentityDbContext<User>
    {
        public Photo_albumDbContext() : base("name=DefaultConnection") { Database.SetInitializer(new CreateDatabaseIfNotExists<Photo_albumDbContext>()); }
        public override IDbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
