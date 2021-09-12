using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Photo_album.DataAccess.Entities;

namespace Photo_album.DataAccess.Context
{
    /// <summary>
    ///     Represents photo album Db context
    /// </summary>
    public class Photo_albumDbContext : IdentityDbContext<User>
    {
        /// <summary>
        ///     Initializes a new instance of the DB context
        /// </summary>
        public Photo_albumDbContext() : base("name=DefaultConnection") { Database.SetInitializer(new CreateDatabaseIfNotExists<Photo_albumDbContext>()); }

        /// <summary>
        ///     Gets or sets users database set
        /// </summary>
        public override IDbSet<User> Users { get; set; }

        /// <summary>
        ///     Gets or sets posts database set
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        ///     Gets or sets comments database set
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        ///     Gets or sets likes database set
        /// </summary>
        public DbSet<Like> Likes { get; set; }

        /// <summary>
        ///     Maps table names, and sets up relationships between the various user entities
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
