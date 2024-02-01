using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;
using ShoppingWeb.Repository.Repositories;
using AutoFixture;
using FluentAssertions;


namespace ShoppingWeb.Repository.Tests.Repositories
{
	public class GenericRepositoryTests : SetupTest 
	{
        // I will test with Product in GenericRepository
        private readonly IGenericRepository<Product> _genericRepository;

        public GenericRepositoryTests()
		{
            _genericRepository = new GenericRepository<Product>(
               _dbContext);
        }

        // test with 1000 record -> getAll  
        [Fact]
        public async Task GenericRepository_GetAllAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var mockData = _fixture.Build<Product>().CreateMany(100).ToList();
            await _dbContext.Products.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _genericRepository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(mockData.Count);
            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task GenericRepository_GetAllAsync_ShouldReturnEmptyWhenHaveNoData()
        {
            var result = await _genericRepository.GetAllAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GenericRepository_GetByIdAsync_ShouldReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<Product>().Create();
            await _dbContext.Products.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _genericRepository.GetByIdAsync(mockData.ProductId);

            // assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(mockData, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GenericRepository_GetByIdAsync_ShouldReturnEmptyWhenHaveNoData()
        {
            var result = await _genericRepository.GetByIdAsync(0);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GenericRepository_AddAsync_ShouldReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<Product>().Create();

            // act
            await _genericRepository.AddAsync(mockData);
            var result = await _dbContext.SaveChangesAsync();

            // assert
            var addedEntity = await _dbContext.Products.FirstOrDefaultAsync(e => e.ProductId == mockData.ProductId);
            addedEntity.Should().NotBeNull();
        }

        [Fact]
        public async Task GenericRepository_AddRangeAsync_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Product>().CreateMany(10).ToList();

            await _genericRepository.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var count = await _dbContext.Products.CountAsync();
            count.Should().Be(10);
        }

        [Fact]
        public async Task GenericRepository_Delete_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Product>().Create();
            _dbContext.Products.AddRange(mockData);
            await _dbContext.SaveChangesAsync();

            _genericRepository.Delete(mockData);

            await _dbContext.SaveChangesAsync();

            var exists = await _dbContext.Products.AnyAsync(x => x.ProductId == mockData.ProductId);
            exists.Should().BeFalse();
        }

        [Fact]
        public async Task GenericRepository_Update_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Product>().Create();
            await _dbContext.Products.AddAsync(mockData);
            await _dbContext.SaveChangesAsync();

            _genericRepository.Update(mockData);
            var result = await _dbContext.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnCorrectDataFirstsPage()
        {
            var mockData = _fixture.Build<Product>().CreateMany(45).ToList();
            await _dbContext.Products.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var pagination = await _genericRepository.ToPagination(0, 10);

            pagination.Previous.Should().BeFalse();
            pagination.Next.Should().BeTrue();
            pagination.Items.Count.Should().Be(10);
            pagination.TotalItemsCount.Should().Be(45);
            pagination.TotalPagesCount.Should().Be(5);
            pagination.PageIndex.Should().Be(0);
            pagination.PageSize.Should().Be(10);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnCorrectDataSecondPage()
        {
            var mockData = _fixture.Build<Product>().CreateMany(45).ToList();
            await _dbContext.Products.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();


            var pagination = await _genericRepository.ToPagination(1, 20);


            pagination.Previous.Should().BeTrue();
            pagination.Next.Should().BeTrue();
            pagination.Items.Count.Should().Be(20);
            pagination.TotalItemsCount.Should().Be(45);
            pagination.TotalPagesCount.Should().Be(3);
            pagination.PageIndex.Should().Be(1);
            pagination.PageSize.Should().Be(20);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnCorrectDataLastPage()
        {
            var mockData = _fixture.Build<Product>().CreateMany(45).ToList();
            await _dbContext.Products.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();


            var pagination = await _genericRepository.ToPagination(2, 20);


            pagination.Previous.Should().BeTrue();
            pagination.Next.Should().BeFalse();
            pagination.Items.Count.Should().Be(5);
            pagination.TotalItemsCount.Should().Be(45);
            pagination.TotalPagesCount.Should().Be(3);
            pagination.PageIndex.Should().Be(2);
            pagination.PageSize.Should().Be(20);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnWithoutData()
        {
            var pagination = await _genericRepository.ToPagination();

            pagination.Previous.Should().BeFalse();
            pagination.Next.Should().BeFalse();
            pagination.Items.Count.Should().Be(0);
            pagination.TotalItemsCount.Should().Be(0);
            pagination.TotalPagesCount.Should().Be(0);
            pagination.PageIndex.Should().Be(0);
        }
    }
}

