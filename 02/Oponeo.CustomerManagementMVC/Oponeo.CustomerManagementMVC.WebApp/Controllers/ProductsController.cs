using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.WebApp.Models;

namespace Oponeo.CustomerManagementMVC.WebApp.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

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

        [Authorize(Roles= "creator")]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                this._productService.AddOrUpdate(new Domain.Models.Product
                {
                    Description = productViewModel.Description,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price
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
