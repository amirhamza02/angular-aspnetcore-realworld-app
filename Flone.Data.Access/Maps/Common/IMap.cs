using Microsoft.EntityFrameworkCore;

namespace Flone.Data.Repository.Maps.Common
{
    public interface IMap
    {
        void Visit(ModelBuilder builder);
    }
}
