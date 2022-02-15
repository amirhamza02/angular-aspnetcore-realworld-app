using Flone.Api.Models.Listing;
using Flone.Data.Models.Listing;

namespace Flone.Data.Queries.Queries.Listing
{
    public interface ICategoryQueryProcessor
    {
        IQueryable<Category> GetQuery();
        IQueryable<Category> Get();
        Category Get(int id);
        Task<Category> Create(CategoryModel model);
        Task<Category> Update(int id, CategoryModel model);
        Task Delete(int id);
    }
}
