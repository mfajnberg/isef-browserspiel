using Microsoft.EntityFrameworkCore;
using web_api.GameModel;
using web_api.GameModel.Creatures;
using web_api.GameModel.OGIs;
using web_api.GameModel.Sites;
using web_api.GameModel.Worldmap;

namespace web_api
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserConfirmation> Confirmations { get; set; }
        public DbSet<HexTile> HexTiles { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<TravelOGI> TravelOGIs { get; set; }
        public DbSet<SiteInteractive> SitesInteractive { get; set; }
        public DbSet<SiteObstacle> SitesObstacle { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Avatar);

            modelBuilder.Entity<HexTile>()
                .HasKey(h => new { h.AxialQ, h.AxialR });
            modelBuilder.Entity<HexTile>().HasOne(h => h.Site);

            modelBuilder.Entity<SiteBase>().HasDiscriminator<SiteType>("Type")
                .HasValue<SiteBase>(SiteType.Empty)
                .HasValue<SiteObstacle>(SiteType.Obstacle)
                .HasValue<SiteInteractive>(SiteType.Interactive);

            modelBuilder.Entity<Party>()
                .HasMany(p => p.Members);

            modelBuilder.Entity<OngoingGameplayInteraction>().HasDiscriminator<InteractionType>("Type").HasValue<TravelOGI>(InteractionType.Travel);
        }
    }
}
