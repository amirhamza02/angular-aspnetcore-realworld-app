using Flone.Data.Models.Listing;
using Flone.Data.Repository.Maps.Common;
using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.Maps.Main
{
    public class CategoryMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .ToTable("Category")
                .HasKey(c => c.Id);
        }
    }
}
