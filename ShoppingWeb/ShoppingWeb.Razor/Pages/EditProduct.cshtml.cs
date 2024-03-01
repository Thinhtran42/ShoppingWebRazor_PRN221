using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingWeb.Repository.Interfaces;
using ShoppingWeb.Repository.Models;

namespace ShoppingWeb.Razor.Pages
{
    public class EditProductModel : PageModel
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

        [BindProperty(SupportsGet = true)]
        public int ProductId { get; set; }

        public EditProductModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            Product = _unitOfWork.ProductRepository.GetByID(ProductId);
            Categories = _unitOfWork.CategoryRepository.Get();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                byte[] imageData = null;
                Product = _unitOfWork.ProductRepository.GetByID(ProductId);

                if (ProductImage != null && ProductImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        ProductImage.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                }
                else if (Product.ProductImage != null)
                {
                    imageData = Product.ProductImage;
                }

                Product.ProductName = ProductName;
                Product.Description = ProductDescription;
                Product.CategoryId = CategoryId;
                Product.UnitPrice = UnitPrice;
                Product.ProductImage = imageData;

                _unitOfWork.ProductRepository.Update(Product);
                _unitOfWork.Save();

                return RedirectToPage("/Pizza");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Error updating product.");
            }

            Categories = _unitOfWork.CategoryRepository.Get();
            return Page();
        }
    }
}
