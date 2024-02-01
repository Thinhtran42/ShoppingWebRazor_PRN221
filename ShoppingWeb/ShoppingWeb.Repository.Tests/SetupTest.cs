using System;
using AutoFixture;
using ShoppingWeb.Repository.Interfaces;
using Moq;
using ShoppingWeb.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingWeb.Repository.Tests
{
	public class SetupTest : IDisposable
	{
        protected readonly Fixture _fixture;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
        protected readonly Mock<IProductRepository> _productRepositoryMock;
        protected readonly Mock<IAccountRepository> _accountRepositoryMock;
        protected readonly ShoppingWebRazorDatabaseContext _dbContext;


        public SetupTest()
		{
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();

            var options = new DbContextOptionsBuilder<ShoppingWebRazorDatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ShoppingWebRazorDatabaseContext(options);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

