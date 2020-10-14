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
    public class SpecialCollectionsController : Controller
    {
        private readonly LibraryDbContext _context;

        public SpecialCollectionsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: SpecialCollections
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialCollection.ToListAsync());
        }

        // GET: SpecialCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialCollection = await _context.SpecialCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialCollection == null)
            {
                return NotFound();
            }

            return View(specialCollection);
        }

        // GET: SpecialCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpecialCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollectionName,Restrictions,ContactInstructions")] SpecialCollection specialCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialCollection);
        }

        // GET: SpecialCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialCollection = await _context.SpecialCollection.FindAsync(id);
            if (specialCollection == null)
            {
                return NotFound();
            }
            return View(specialCollection);
        }

        // POST: SpecialCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CollectionName,Restrictions,ContactInstructions")] SpecialCollection specialCollection)
        {
            if (id != specialCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialCollectionExists(specialCollection.Id))
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
            return View(specialCollection);
        }

        // GET: SpecialCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialCollection = await _context.SpecialCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialCollection == null)
            {
                return NotFound();
            }

            return View(specialCollection);
        }

        // POST: SpecialCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialCollection = await _context.SpecialCollection.FindAsync(id);
            _context.SpecialCollection.Remove(specialCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialCollectionExists(int id)
        {
            return _context.SpecialCollection.Any(e => e.Id == id);
        }
    }
}
