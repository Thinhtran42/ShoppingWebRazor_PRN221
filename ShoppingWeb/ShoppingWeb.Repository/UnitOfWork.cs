using System;
using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;
using ShoppingWeb.Repository.Repositories;

namespace ShoppingWeb.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ShoppingWebRazorDatabaseContext _context;
        private GenericRepository<Account> accountRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<Customer> customerRepository;

        public UnitOfWork(ShoppingWebRazorDatabaseContext context)
        {
            _context = context;
        }

        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new GenericRepository<Account>(_context);
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
                    productRepository = new GenericRepository<Product>(_context);
                }
                return productRepository;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new GenericRepository<Customer>(_context);
                }
                return customerRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
