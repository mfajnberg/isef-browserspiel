using Microsoft.EntityFrameworkCore;
using web_api.GameModel;
using web_api.GameModel.AvatarModel;

namespace web_api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet <User> Users { get; set; }
        public DbSet <Avatar> Avatars { get; set; }
    }
}
