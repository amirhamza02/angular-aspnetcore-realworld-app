using Flone.Data.Models;
using Flone.Data.Models.Listing;
using Flone.Data.Queries.Queries.Listing;
using Flone.Data.Repository.DAL;
using Flone.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Flone.Api.Models.Listing;
using Flone.Api.Common.Exceptions;

namespace Flone.Queries.Test.Listing
{
    public class CategoryQueryProcessorTest
    {
        private Mock<IUnitOfWork> _uow;
        private List<Category> _categoryList;
        private ICategoryQueryProcessor _query;
        private Random _random;
        private User _currentUser;
        private Mock<ISecurityContext> _securityContext;

        public CategoryQueryProcessorTest()
        {
            _random = new Random();
            _uow = new Mock<IUnitOfWork>();
            _categoryList = new List<Category>();

            _uow.Setup(x => x.Query<Category>()).Returns(() => _categoryList.AsQueryable());
            _currentUser = new User{ Id =_random.Next()};
            _securityContext = new Mock<ISecurityContext>(MockBehavior.Strict);
            _securityContext.Setup(x=>x.User).Returns(_currentUser);
            _securityContext.Setup(x=>x.IsAdministrator).Returns(false);
            _query = new CategoryQueryProcessor(_uow.Object,_securityContext.Object);
        }

        [Fact]
        public void GetShouldReturnAll()
        {
            _categoryList.Add(new Category { Id= _random.Next()});
            var result = _query.Get().ToList();
            result.Count.Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnAllActiveCategories()
        {
            _categoryList.Add(new Category { Id = _random.Next(),IsDeleted=false });
            _categoryList.Add(new Category { Id = _random.Next(),IsDeleted=true });
            var result = _query.GetQuery().ToList();
            result.Count.Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnOnlyUserCategory()
        {
            _categoryList.Add(new Category { Id= _random.Next()});
            _categoryList.Add(new Category { Id = _currentUser.Id });
            var result = _query.Get(_currentUser.Id);
            result.Id.Should().Be(_currentUser.Id);
        }

        [Fact]
        public async void CreateShouldSaveNew()
        {
            var category = new CategoryModel
            {
                Id = _random.Next(),
                CatgoryName = _random.Next().ToString(),
                MarketId = _random.Next(),
            };

            var result = await _query.Create(category);

            result.Id.Should().Be(category.Id);
            result.CatgoryName.Should().Be(category.CatgoryName);
            result.MarketId.Should().Be(category.MarketId);

            _uow.Verify(x => x.Add(result));
            _uow.Verify(x => x.CommitAsync());
        }


        [Fact]
        public async void UpdateShouldUpdateFields()
        {
            var cateGory = new Category
            {
                CatgoryName = _random.Next().ToString(),
                Id = _random.Next(),
                MarketId = _random.Next(),
            };

            _categoryList.Add(cateGory);
            
            var cateGoryModel = new CategoryModel
            {
                CatgoryName = _random.Next().ToString(),
                Id = _random.Next(),
                MarketId = _random.Next(),
            };

            var result = await _query.Update(cateGory.Id, cateGoryModel);

            result.CatgoryName.Should().Be(result.CatgoryName);
            result.MarketId.Should().Be(result.MarketId);

            _uow.Verify(x => x.CommitAsync());
        }

        [Fact]
        public void UpdateShoudlThrowExceptionIfItemIsNotFound()
        {
            Action create = () =>
            {
                var result = _query.Update(_random.Next(), new CategoryModel()).Result;
            };
            
            create.Should().Throw<NotFoundException>();
        }




    }
}
