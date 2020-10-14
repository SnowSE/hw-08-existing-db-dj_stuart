using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryDatabaseHW8.Data;

namespace LibraryDatabaseHW8.Controllers
{
    public class CheckoutItemsController : Controller
    {
        private readonly LibraryDbContext _context;

        public CheckoutItemsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: CheckoutItems
        public async Task<IActionResult> Index()
        {
            var libraryDbContext = _context.CheckoutItem.Include(c => c.Book).Include(c => c.Checkout);
            return View(await libraryDbContext.ToListAsync());
        }

        // GET: CheckoutItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkoutItem = await _context.CheckoutItem
                .Include(c => c.Book)
                .Include(c => c.Checkout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkoutItem == null)
            {
                return NotFound();
            }

            return View(checkoutItem);
        }

        // GET: CheckoutItems/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            ViewData["CheckoutId"] = new SelectList(_context.Checkout, "Id", "Id");
            return View();
        }

        // POST: CheckoutItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,CheckoutId,Returned")] CheckoutItem checkoutItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkoutItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", checkoutItem.BookId);
            ViewData["CheckoutId"] = new SelectList(_context.Checkout, "Id", "Id", checkoutItem.CheckoutId);
            return View(checkoutItem);
        }

        // GET: CheckoutItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkoutItem = await _context.CheckoutItem.FindAsync(id);
            if (checkoutItem == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", checkoutItem.BookId);
            ViewData["CheckoutId"] = new SelectList(_context.Checkout, "Id", "Id", checkoutItem.CheckoutId);
            return View(checkoutItem);
        }

        // POST: CheckoutItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,CheckoutId,Returned")] CheckoutItem checkoutItem)
        {
            if (id != checkoutItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkoutItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckoutItemExists(checkoutItem.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", checkoutItem.BookId);
            ViewData["CheckoutId"] = new SelectList(_context.Checkout, "Id", "Id", checkoutItem.CheckoutId);
            return View(checkoutItem);
        }

        // GET: CheckoutItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkoutItem = await _context.CheckoutItem
                .Include(c => c.Book)
                .Include(c => c.Checkout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkoutItem == null)
            {
                return NotFound();
            }

            return View(checkoutItem);
        }

        // POST: CheckoutItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkoutItem = await _context.CheckoutItem.FindAsync(id);
            _context.CheckoutItem.Remove(checkoutItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckoutItemExists(int id)
        {
            return _context.CheckoutItem.Any(e => e.Id == id);
        }
    }
}
