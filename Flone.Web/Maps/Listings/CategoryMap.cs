using AutoMapper;
using Flone.Api.Models.Listing;
using Flone.Data.Models.Listing;

namespace Flone.Web.Maps.Listings
{
    public class CategoryMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<Category, CategoryModel>();
        }
    }
}
