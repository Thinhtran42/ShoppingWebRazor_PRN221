using System;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingWebRazorDatabaseContext _dbContext;
        private readonly IProductRepository _productRepo;
        private readonly IAccountRepository _accountRepo;

        public UnitOfWork(ShoppingWebRazorDatabaseContext dbContext, IProductRepository productRepo, IAccountRepository accountRepo)
        {
            _dbContext = dbContext;
            _productRepo = productRepo;
            _accountRepo = accountRepo;
        }

        public IAccountRepository AccountRepository => _accountRepo;

        public IProductRepository ProductRepository => _productRepo;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

