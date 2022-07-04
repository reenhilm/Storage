using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageContext _context;

        public ProductsController(StorageContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'StorageContext.Product'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult List()
        {
            if(_context.Product == null)
                return NotFound();

            var products = _context.Product
                .Select(product =>
                        new ProductViewModel
                        {
                            Name = product.Name,
                            Id = product.Id,
                            Count = product.Count,
                            Price = product.Price                            
                        })
                .ToList();

            if (products == null)
                return NotFound();

            foreach (ProductViewModel p in products)
                p.InventoryValue = (p.Price * p.Count);

            var lwm = new ListViewModel();
            lwm.ProductList = products;

            var productCategories = _context.Product
                            .Select(product => product.Category)
                            .ToList();

            if (productCategories == null)
                return NotFound();

            lwm.CategoryList = productCategories;
            return View(lwm);
        }
        public IActionResult Search(string category, string productName)
        {
            if (category == null && productName == null)
                return RedirectToAction(nameof(List));

            if (_context.Product == null)
                return NotFound();

            IQueryable<Product> productsQuery = _context.Product.AsQueryable()
                .WhereIf(category != null, x => x.Category == category)
                .WhereIf(productName != null, x => x.Name.StartsWith(productName!));

            var products = productsQuery
                .Select(product =>
                        new ProductViewModel
                        {
                            Name = product.Name,
                            Id = product.Id,
                            Count = product.Count,
                            Price = product.Price
                        })
                .ToList();

            if (products == null)
            {
                return NotFound();
            }

            foreach (ProductViewModel p in products)
                p.InventoryValue = (p.Price * p.Count);

            var lwm = new ListViewModel();
            lwm.ProductList = products;

            var productCategories = _context.Product
                            .Select(product => product.Category)
                            .ToList();

            if (productCategories == null)
                return NotFound();

            lwm.CategoryList = productCategories;

            return View(nameof(List), lwm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'StorageContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
