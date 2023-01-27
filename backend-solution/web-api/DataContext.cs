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
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HexTile>().HasKey(h => new { h.AxialQ, h.AxialR });

            modelBuilder.Entity<Creature>().HasOne(c => c.Fellowship).WithMany(p => p.Members).HasForeignKey(c => c.FellowshipId);

        }

        public DbSet <User> Users { get; set; }
        public DbSet <Avatar> Avatars { get; set; }

        public DbSet<Creature> Creatures { get; set; }

        public DbSet<Party> Parties { get; set; }

        public DbSet<UserConfirmation> Confirmations { get; set; }

        public DbSet<OngoingGameplayInteraction> OGIs { get; set; }

        public DbSet<HexTile> HexTiles { get; set; }

        public DbSet<SiteInteractive> SitesInteractive { get; set; }

        public DbSet<SiteObstacle> SitesObstacle { get; set; }

    }
}
