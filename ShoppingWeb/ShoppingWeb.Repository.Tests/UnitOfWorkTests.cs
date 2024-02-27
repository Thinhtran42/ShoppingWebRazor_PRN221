//using System;
//using AutoFixture;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using ShoppingWeb.Repository.Interfaces;
//using ShoppingWeb.Repository.Models;

//namespace ShoppingWeb.Repository.Tests
//{
//	public class UnitOfWorkTests : SetupTest
//	{
//        private readonly IUnitOfWork _unitOfWork;
//        public UnitOfWorkTests()
//        {
//            _unitOfWork = new UnitOfWork(_dbContext);
//        }

//        [Fact]
//        public async Task TestUnitOfWork()
//        {
//            // Arrange
//            var mockData = _fixture.Build<Account>().CreateMany(10).ToList();
//            _accountRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockData);

//            // Act
//            var items = await _unitOfWork.AccountRepository.GetAllAsync();

//            // Assert
//            items.Should().NotBeNull();
//            items.Should().HaveCount(mockData.Count);
//            items.Should().BeEquivalentTo(mockData);
//        }
//    }
//}

