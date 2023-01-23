using Microsoft.EntityFrameworkCore;
using web_api.GameModel;
using web_api.GameModel.AvatarModel;
using web_api.GameModel.Creatures;
using web_api.GameModel.Sites;

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
        }

        public DbSet <User> Users { get; set; }
        public DbSet <Avatar> Avatars { get; set; }

        public DbSet<UserConfirmation> Confirmation { get; set; }

        public DbSet<OngoingInteraction> Interactions { get; set; }

        public DbSet<HexTile> HexTiles { get; set; }

        public DbSet<SiteInteractive> SitesInteractive { get; set; }

        public DbSet<SiteObstacle> SitesObstacle { get; set; }

    }
}
