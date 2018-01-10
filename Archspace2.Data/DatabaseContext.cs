using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Archspace2
{
    public class DatabaseContext : DbContext
    {
        protected string mConnectionString;

        public DatabaseContext(string aConnectionString)
        {
            mConnectionString = aConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder aDbContextOptionsBuilder)
        {
            if (!aDbContextOptionsBuilder.IsConfigured)
            {
                aDbContextOptionsBuilder.UseSqlServer(mConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder aModelBuilder)
        {
            foreach (var entityType in aModelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(aModelBuilder);
        }

        public DbSet<Universe> Universes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
