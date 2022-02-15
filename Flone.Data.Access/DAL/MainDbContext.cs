using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.DAL
{
    public class MainDbContext :DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) :base(options)
        {

        }

        //public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Maps.Common.IMap>? mappings = MappingsHelper.GetMainMappings();

            foreach (var mapping in mappings)
            {
                mapping.Visit(modelBuilder);
            }
        }
    }
}
