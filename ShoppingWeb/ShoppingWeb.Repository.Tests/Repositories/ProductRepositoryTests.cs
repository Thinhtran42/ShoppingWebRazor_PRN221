using System;
using AutoFixture;
using FluentAssertions;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;
using ShoppingWeb.Repository.Repositories;

namespace ShoppingWeb.Repository.Tests.Repositories
{
	public class ProductRepositoryTests : SetupTest
	{
        private readonly IProductRepository _productRepository;

        public ProductRepositoryTests()
		{
			_productRepository = new ProductRepository(_dbContext);
		}

		[Fact]
		public async Task ProductRepository_GetTop3HighestPricedProducts_ShouldReturnCorrectData()
		{
			// arrange
			var mockData = _fixture.Build<Product>().CreateMany(10).ToList();
            await _dbContext.Products.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

			// act
			var top3_highestPrice = mockData.Take(3).OrderByDescending(x => x.UnitPrice).ToList();
			var result = _productRepository.GetTop3HighestPricedProducts();

			// assert
			result.Should().BeEquivalentTo(top3_highestPrice);
        }

    }
}

