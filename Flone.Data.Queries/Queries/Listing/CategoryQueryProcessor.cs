using Flone.Api.Common.Exceptions;
using Flone.Api.Models.Listing;
using Flone.Data.Models.Listing;
using Flone.Data.Repository.DAL;
using Flone.Security;

namespace Flone.Data.Queries.Queries.Listing
{
    public class CategoryQueryProcessor : ICategoryQueryProcessor
    {
        private readonly IUnitOfWork _uow;
        private readonly ISecurityContext _securityContext;

        public CategoryQueryProcessor(IUnitOfWork uow, ISecurityContext securityContext)
        {
            _uow = uow;
            _securityContext= securityContext;
        }
        public async Task<Category> Create(CategoryModel model)
        {

           Category category = new Category()
           {
               Id = model.Id,
               UserCreated = _securityContext.User.Id.ToString(),
               CatgoryName = model.CatgoryName,
               MarketId=model.MarketId
           };

            _uow.Add(category);
            await _uow.CommitAsync();
            return category;
        }

        public async Task Delete(int id)
        {
            var category = this.GetQuery().FirstOrDefault(x=> x.Id == id);
            if (category == null)
            {
                throw new NotFoundException("User is not found");
            }

            if(category.IsDeleted) return ;

            category.IsDeleted = true;
            await _uow.CommitAsync();
        }

        public IQueryable<Category> GetQuery()
        {
            var q = _uow.Query<Category>().Where(p => !p.IsDeleted);
            return q;
        }

        public IQueryable<Category> Get()
        {
            return this.GetQuery();
        }

        public Category Get(int id)
        {
            var category= _uow.Query<Category>().FirstOrDefault(p=>p.Id == id);

            if (category == null)
            {
                throw new NotFoundException("Category is not found");
            }

            return category;
        }

        public async Task<Category> Update(int id, CategoryModel model)
        {
            var category = _uow.Query<Category>().FirstOrDefault(x=>x.Id == id);

            if(category == null)
            {
                throw new NotFoundException("Category not found");
            }

            category.CatgoryName = model.CatgoryName;
            category.MarketId= model.MarketId;

            await _uow.CommitAsync();

            return category;
        }
    }
}
