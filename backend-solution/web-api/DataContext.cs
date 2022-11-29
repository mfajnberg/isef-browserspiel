using Microsoft.EntityFrameworkCore;
using web_api.GameModel;

namespace web_api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet <User> Users { get; set; }
    }
}
