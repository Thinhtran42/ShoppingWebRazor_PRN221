//using System;
//using AutoFixture;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using ShoppingWeb.Repository.Models;

//namespace ShoppingWeb.Repository.Tests.Models
//{
//	public class ShoppingWebRazorDatabaseContextTests : SetupTest, IDisposable
//    {
//        [Fact]
//        public async Task ShoppingWebRazorDatabaseContextTests_AccountsDbSetShouldReturnCorrectData()
//        {
//            // arrange
//            var mockData = _fixture.Build<Account>().CreateMany(10).ToList();
//            await _dbContext.Accounts.AddRangeAsync(mockData);
//            await _dbContext.SaveChangesAsync();

//            // act
//            var result = await _dbContext.Accounts.ToListAsync();

//            // assert
//            result.Should().BeEquivalentTo(mockData);
//        }

//        [Fact]
//        public async Task ShoppingWebRazorDatabaseContextTests_AccountssDbSetShouldReturnEmptyListWhenNotHavingData()
//        {
//            var result = await _dbContext.Accounts.ToListAsync();
//            result.Should().BeEmpty();
//        }
//    }
//}

