using Lab2OPP.Data;
using Lab2OPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab2OPP.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderItemsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.OrderItems
                .AsNoTracking()
                .Include(i => i.Order)
                    .ThenInclude(o => o.User)
                .Include(i => i.Product)
                .OrderByDescending(i => i.Id)
                .ToListAsync();

            return View(items);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.OrderItems
                .AsNoTracking()
                .Include(i => i.Order)
                    .ThenInclude(o => o.User)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return NotFound();

            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            await FillSelectLists();
            return View(new OrderItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Quantity")] OrderItem item)
        {
            if (!ModelState.IsValid)
            {
                await FillSelectLists(item.OrderId, item.ProductId);
                return View(item);
            }

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();

            await FillSelectLists(item.OrderId, item.ProductId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId,Quantity")] OrderItem item)
        {
            if (id != item.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await FillSelectLists(item.OrderId, item.ProductId);
                return View(item);
            }

            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.OrderItems.AnyAsync(i => i.Id == item.Id);
                if (!exists) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.OrderItems
                .AsNoTracking()
                .Include(i => i.Order)
                    .ThenInclude(o => o.User)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item != null)
            {
                _context.OrderItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        private async Task FillSelectLists(int? selectedOrderId = null, int? selectedProductId = null)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.User)
                .OrderByDescending(o => o.Date)
                .ToListAsync();

            var orderList = orders.Select(o => new
            {
                o.Id,
                Text = $"Order #{o.Id} ({o.User?.Name ?? "Unknown"})"
            }).ToList();

            var products = await _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();

            ViewBag.OrderId = new SelectList(orderList, "Id", "Text", selectedOrderId);
            ViewBag.ProductId = new SelectList(products, "Id", "Name", selectedProductId);
        }
    }
}