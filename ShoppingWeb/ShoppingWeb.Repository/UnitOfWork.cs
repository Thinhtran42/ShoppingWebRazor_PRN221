using System;
using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;
using ShoppingWeb.Repository.Repositories;

namespace ShoppingWeb.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ShoppingWebRazorDatabaseContext context = new ShoppingWebRazorDatabaseContext();
        private GenericRepository<Account> accountRepository;
        private GenericRepository<Product> productRepository;

        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new GenericRepository<Account>(context);
                }
                return accountRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

