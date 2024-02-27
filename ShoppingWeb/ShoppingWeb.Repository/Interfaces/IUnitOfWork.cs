using System;
using ShoppingWeb.Repository.Models;
using ShoppingWeb.Repository.Repositories;

namespace ShoppingWeb.Repository.Interfaces
{
	public interface IUnitOfWork
	{
        GenericRepository<Account> AccountRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        void Save();
    }
}

