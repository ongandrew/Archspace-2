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
            aModelBuilder.Entity<Admiral>().OwnsOne(x => x.BaseSkills);

            aModelBuilder.Entity<Council>().OwnsOne(x => x.Resource);

            aModelBuilder.Entity<CouncilRelation>()
                .HasOne(x => x.FromCouncil)
                .WithMany(x => x.FromRelations);

            aModelBuilder.Entity<CouncilRelation>()
                .HasOne(x => x.ToCouncil)
                .WithMany(x => x.ToRelations);

            aModelBuilder.Entity<Fleet>().OwnsOne(x => x.Mission);
            
            aModelBuilder.Entity<Planet>().OwnsOne(x => x.Atmosphere);
            aModelBuilder.Entity<Planet>().OwnsOne(x => x.DistributionRatio);
            aModelBuilder.Entity<Planet>().OwnsOne(x => x.Infrastructure);
            aModelBuilder.Entity<Planet>().HasMany(x => x.CommercePlanets).WithOne();

            aModelBuilder.Entity<Player>().OwnsOne(x => x.Resource);

            aModelBuilder.Entity<PlayerEffectInstance>().OwnsOne(x => x.ControlModelModifier);

            aModelBuilder.Entity<PlayerRelation>()
                .HasOne(x => x.FromPlayer)
                .WithMany(x => x.FromRelations);

            aModelBuilder.Entity<PlayerRelation>()
                .HasOne(x => x.ToPlayer)
                .WithMany(x => x.ToRelations);

            foreach (var entityType in aModelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(aModelBuilder);
        }

        public DbSet<Admiral> Admirals { get; set; }
        public DbSet<BlackMarket> BlackMarkets { get; set; }
        public DbSet<BlackMarketItem> BlackMarketItems { get; set; }
        public DbSet<Council> Councils { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<DefenseDeployment> DefenseDeployments { get; set; }
        public DbSet<DefensePlan> DefensePlans { get; set; }
        public DbSet<Fleet> Fleets { get; set; }
        public DbSet<Mailbox> Mailboxes { get; set; }
        public DbSet<MailItem> MailItems { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<ShipDesign> ShipDesigns { get; set; }
        public DbSet<Universe> Universes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<SystemLog> SystemLogs { get; set; }
    }
}
