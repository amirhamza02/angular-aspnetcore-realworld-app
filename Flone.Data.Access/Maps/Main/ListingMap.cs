using Flone.Data.Models.Listing;
using Flone.Data.Repository.Maps.Common;
using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.Maps.Main
{
    public class ListingMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Listing>()
                 .ToTable("Listing")
                 .HasKey(c => c.Id);
        }
    }
}
