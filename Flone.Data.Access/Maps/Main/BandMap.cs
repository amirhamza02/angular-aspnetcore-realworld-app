using Flone.Data.Models.Listing;
using Flone.Data.Repository.Maps.Common;
using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.Maps.Main
{
    public class BandMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Brand>()
                .ToTable("Brand")
                .HasKey(c => c.Id);
        }
    }
}
