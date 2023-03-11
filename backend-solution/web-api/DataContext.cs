using Microsoft.EntityFrameworkCore;
using web_api.GameModel;
using web_api.GameModel.Creatures;
using web_api.GameModel.OGIs;
using web_api.GameModel.Sites;
using web_api.GameModel.Worldmap;

namespace web_api
{
    /// <summary>
    /// DataContext for the Database
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Users Table
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Confirmation Table
        /// </summary>
        public DbSet<UserConfirmation> Confirmations { get; set; }
        /// <summary>
        /// HexTiles Table
        /// </summary>
        public DbSet<HexTile> HexTiles { get; set; }

        /// <summary>
        /// Creatures Table
        /// </summary>
        public DbSet<Creature> Creatures { get; set; }

        /// <summary>
        /// Avatars Table
        /// </summary>
        public DbSet<Avatar> Avatars { get; set; }

        /// <summary>
        /// Parties Table
        /// </summary>
        public DbSet<Party> Parties { get; set; }

        /// <summary>
        /// TravelOGI Table
        /// </summary>
        public DbSet<TravelOGI> TravelOGIs { get; set; }

        /// <summary>
        /// interactive Sites Table
        /// </summary>
        public DbSet<SiteInteractive> SitesInteractive { get; set; }

        /// <summary>
        /// abstacle sites tabel
        /// </summary>
        public DbSet<SiteObstacle> SitesObstacle { get; set; }

        /// <summary>
        /// chest sites table
        /// </summary>
        public DbSet<Chest> SitesChest { get; set; }

        /// <summary>
        /// Contstructor of DataContext
        /// </summary>
        /// <param name="options">options</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /// <summary>
        /// overwritten method for custom attributes 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // the user has one avatar
            modelBuilder.Entity<User>()
                .HasOne(u => u.Avatar);

            modelBuilder.Entity<Creature>()
                .HasKey(c => c.Id);

            // the HexTile needs Primiray key to Q and R coordinates
            modelBuilder.Entity<HexTile>()
                .HasKey(h => new { h.AxialQ, h.AxialR });
            modelBuilder.Entity<HexTile>().HasOne(h => h.Site);

            // Sitebase has a reference to SiteType enum
            modelBuilder.Entity<SiteBase>()
                .HasDiscriminator<SiteType>("Type")
                .HasValue<SiteBase>(SiteType.Empty)
                .HasValue<SiteObstacle>(SiteType.Obstacle)
                .HasValue<SiteInteractive>(SiteType.Interactive)
                .HasValue<Forest>(SiteType.Forest)
                .HasValue<Banner>(SiteType.Banner)
                .HasValue<Camp>(SiteType.Camp)
                .HasValue<House>(SiteType.House)
                .HasValue<Chest>(SiteType.Chest)
                .HasValue<Crystal>(SiteType.Crystal)
                .HasValue<AncientTree>(SiteType.AncientTree);

            // one Party can own more memebers
            modelBuilder.Entity<Party>()
                .HasMany(p => p.Members);
            modelBuilder.Entity<Party>()
                .HasOne<OngoingGameplayInteraction>();

            // OGI has a reference to InteractionType enum 
            modelBuilder.Entity<OngoingGameplayInteraction>()
                .HasDiscriminator<InteractionType>("Type")
                .HasValue<TravelOGI>(InteractionType.Travel);
            modelBuilder.Entity<OngoingGameplayInteraction>()
                .HasIndex(o => o.PartyId)
                .IsUnique(false);
        }
    }
}
