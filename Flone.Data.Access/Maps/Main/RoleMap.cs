using Flone.Data.Models;
using Flone.Data.Repository.Maps.Common;
using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.Maps.Main
{
    public class RoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(x => x.Id);
        }
    }
}
