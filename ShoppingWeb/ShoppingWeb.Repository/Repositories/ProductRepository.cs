using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Repository.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ShoppingWebRazorDatabaseContext _dbContext;

        public ProductRepository(ShoppingWebRazorDatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // this is how we can create custom method with repository pattern
        public List<Product> GetTop3HighestPricedProducts()
        {
            return _dbContext.Products.Take(3).OrderByDescending(x => x.UnitPrice).ToList();
        }
    }
}

