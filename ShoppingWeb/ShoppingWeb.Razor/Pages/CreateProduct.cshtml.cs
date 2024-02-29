using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Pages
{
    public class CreateProductModel : PageModel
    {
        protected IUnitOfWork _unitOfWork;

        public Product Product { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        [BindProperty]
        public string ProductName { get; set; }

        [BindProperty]
        public string ProductDescription { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        [BindProperty]
        public decimal? UnitPrice { get; set; }

        [BindProperty]
        public IFormFile ProductImage { get; set; }

        public CreateProductModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            Categories = _unitOfWork.CategoryRepository.Get();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                byte[] imageData = null;

                if (ProductImage != null && ProductImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        ProductImage.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                }

                var products = _unitOfWork.ProductRepository.Get();
                _unitOfWork.ProductRepository.Insert(new Product
                {
                    ProductName = ProductName,
                    Description = ProductDescription,
                    CategoryId = CategoryId,
                    UnitPrice = UnitPrice,
                    ProductImage = imageData,
                    SupplierId = 1,
                    QuantityPerUnit = "",
                    IsDeleted = false
                });

                _unitOfWork.Save();

                return RedirectToPage("/Pizza");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error creating product.");
            }

            Categories = _unitOfWork.CategoryRepository.Get();
            return Page();
        }
    }
}
