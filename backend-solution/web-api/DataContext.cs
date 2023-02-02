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

            modelBuilder.Entity<HexTile>()
                .HasKey(hex => new { hex.AxialQ, hex.AxialR });
            modelBuilder.Entity<HexTile>().HasOne(hex => hex.Site);

            modelBuilder.Entity<SiteBase>().HasDiscriminator<SiteType>("Type")
                .HasValue<SiteBase>(SiteType.Empty)
                .HasValue<SiteObstacle>(SiteType.Obstacle)
                .HasValue<SiteInteractive>(SiteType.Interactive);
            //modelBuilder.Entity<SiteBase>().Property(site => site.Type)
            //    .HasConversion(enumValue => enumValue.ToString(), stringRep => (SiteType)Enum.Parse(typeof(SiteType), stringRep));

            modelBuilder.Entity<Creature>().HasOne(creature => creature.Party).WithMany(party => party.Members);

            modelBuilder.Entity<Party>().HasOne(party => party.Leader);

            modelBuilder.Entity<OngoingGameplayInteraction>().HasDiscriminator<InteractionType>("Type").HasValue<TravelOGI>(InteractionType.Travel);
            //modelBuilder.Entity<OngoingGameplayInteraction>().Property(ogi => ogi.Type)
            //    .HasConversion(enumValue => enumValue.ToString(), stringRep => (InteractionType)Enum.Parse(typeof(InteractionType), stringRep));
        }

        public DbSet <User> Users { get; set; }
        public DbSet <Avatar> Avatars { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<UserConfirmation> Confirmations { get; set; }
        public DbSet<TravelOGI> TravelOGIs { get; set; }
        public DbSet<HexTile> HexTiles { get; set; }
        public DbSet<SiteInteractive> SitesInteractive { get; set; }
        public DbSet<SiteObstacle> SitesObstacle { get; set; }

    }
}
