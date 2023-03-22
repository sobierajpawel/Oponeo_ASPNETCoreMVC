using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.WebApp.Infrastructure;
using Oponeo.CustomerManagementMVC.WebApp.Models;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly IMemoryCache _memoryCache;
        private const int PAGE_SIZE = 3;

        public ProductsController(ProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Search()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString, int pageNumber)
        {
            string memoryKey = $"{searchString}_{pageNumber}";

            if (!_memoryCache.TryGetValue(memoryKey, out PaginatedProductViewModel paginatedProductViewModel))
            {
                paginatedProductViewModel = new();
                foreach (var product in _productService.FindByFilter(searchString))
                {
                    paginatedProductViewModel.ProductViewModels.Add(new ProductViewModel
                    {
                        Description = product.Description,
                        Name = product.Name,
                        Price = product.Price,
                        Id = product.Id
                    });
                }

                double differnece = (double)paginatedProductViewModel.ProductViewModels.Count() / PAGE_SIZE;
                paginatedProductViewModel.TotalPages = (int)Math.Ceiling(differnece);
                paginatedProductViewModel.ProductViewModels = paginatedProductViewModel
                    .ProductViewModels
                    .Skip(pageNumber * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .ToList();

                var memoryCacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));

                _memoryCache.Set(memoryKey, paginatedProductViewModel, memoryCacheOption);
            }

            return PartialView("GetProductsWithPagination", paginatedProductViewModel);
        }

        [ServiceFilter(typeof(LoggingFilter))]
        // GET: Products
        public async Task<IActionResult> Index()
        {
            IList<ProductViewModel> productsViewModels = new List<ProductViewModel>();
            foreach (var product in _productService.Get())
            {
                productsViewModels.Add(new ProductViewModel
                {
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    Id = product.Id
                });
            }
                    
            return View(productsViewModels);
        }


        // GET: Products/Details/5
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // SELECT * FROM Products WHERE id = @id
            var product = this._productService.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = new()
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };

            return View(productViewModel);
        }

        [ServiceFilter(typeof(LoggingFilter))]
        [Authorize(Policy = "RequiredCreatorRole")]
        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.ProductTypes = _productService.GetProductTypes()
                .Select(x => new SelectListItem(x.TypeName, x.Id.ToString()));
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "RequireModuleProducts")]
        [Authorize(Policy = "RequiredCreatorRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price, ProductTypeId")] ProductViewModel productViewModel)
        {

            if (ModelState.IsValid)
            {
                this._productService.AddOrUpdate(new Domain.Models.Product
                {
                    Description = productViewModel.Description,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price,
                    ProductTypeId = productViewModel.ProductTypeId,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _productService.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = new()
            {
                Description = product.Description,
                Name = product.Name,
                Id = product.Id,
                Price = product.Price
            };

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description, Price")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                this._productService.AddOrUpdate(new Domain.Models.Product
                {
                    Description = productViewModel.Description,
                    Name = productViewModel.Name,
                    Id = productViewModel.Id,
                    Price = productViewModel.Price
                });
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel productViewModel = new()
            {
                Description = product.Description,
                Name = product.Name,
                Id = product.Id
            };

            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _productService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
