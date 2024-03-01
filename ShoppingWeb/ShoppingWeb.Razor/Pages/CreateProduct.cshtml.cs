using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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

                var loginUserJson = HttpContext.Session.GetString("loginUser");
                var loginUser = new Account();
                if (loginUserJson != null)
                {
                    loginUser = JsonConvert.DeserializeObject<Account>(loginUserJson);
                }

                // var supplier = new Supplier()
                // {
                //     CompanyName = "Pizza Company",
                //     Address = "Ho Chi Minh city",
                //     AccountId = loginUser.AccountId
                // };

                // _unitOfWork.SupplierRepository.Insert(supplier);
                // _unitOfWork.Save();

                var supplier = _unitOfWork.SupplierRepository.Get(supplier => supplier.AccountId == loginUser.AccountId).FirstOrDefault();

                var products = _unitOfWork.ProductRepository.Get();

                _unitOfWork.ProductRepository.Insert(new Product
                {
                    ProductName = ProductName,
                    Description = ProductDescription,
                    CategoryId = CategoryId,
                    UnitPrice = UnitPrice,
                    ProductImage = imageData,
                    SupplierId = supplier.SupplierId,
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
