using System;
namespace ShoppingWeb.Repository.Interfaces
{
	public interface IUnitOfWork
	{
        public IAccountRepository AccountRepository { get; }
        public IProductRepository ProductRepository { get; }

        public Task<int> SaveChangeAsync();
    }
}

