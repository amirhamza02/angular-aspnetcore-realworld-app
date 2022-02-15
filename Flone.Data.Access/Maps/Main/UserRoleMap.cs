using Flone.Data.Models;
using Flone.Data.Repository.Maps.Common;
using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.Maps.Main
{
    public class UserRoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                 .ToTable("UserRoles")
                 .HasKey(x => x.Id);
                
        }
    }
}   
