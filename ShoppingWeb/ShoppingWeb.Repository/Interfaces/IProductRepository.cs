using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Repository.Interfaces
{
	public interface IProductRepository : IGenericRepository<Product>
    {
		public List<Product> GetTop3HighestPricedProducts();
	}
}

