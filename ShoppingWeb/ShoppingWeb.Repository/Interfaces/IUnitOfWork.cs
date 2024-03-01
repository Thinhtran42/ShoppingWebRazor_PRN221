using ShoppingWeb.Repository.Models;
using ShoppingWeb.Repository.Repositories;

namespace ShoppingWeb.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<Account> AccountRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<Supplier> SupplierRepository { get; }
        void Save();
    }
}
