using Flone.Data.Models.Listing;
using Flone.Data.Repository.Maps.Common;
using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.Maps.Main
{
    public class SubcategoryMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Subcategory>()
            .ToTable("Subcategory")
            .HasKey(x => x.Id);
        }
    }
}
