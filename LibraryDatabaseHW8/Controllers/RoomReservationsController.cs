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
    public class RoomReservationsController : Controller
    {
        private readonly LibraryDbContext _context;

        public RoomReservationsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: RoomReservations
        public async Task<IActionResult> Index()
        {
            var libraryDbContext = _context.RoomReservation.Include(r => r.Room).Include(r => r.Student);
            return View(await libraryDbContext.ToListAsync());
        }

        // GET: RoomReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomReservation = await _context.RoomReservation
                .Include(r => r.Room)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomReservation == null)
            {
                return NotFound();
            }

            return View(roomReservation);
        }

        // GET: RoomReservations/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: RoomReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,RoomId,StudentId")] RoomReservation roomReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", roomReservation.RoomId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", roomReservation.StudentId);
            return View(roomReservation);
        }

        // GET: RoomReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomReservation = await _context.RoomReservation.FindAsync(id);
            if (roomReservation == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", roomReservation.RoomId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", roomReservation.StudentId);
            return View(roomReservation);
        }

        // POST: RoomReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,RoomId,StudentId")] RoomReservation roomReservation)
        {
            if (id != roomReservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomReservationExists(roomReservation.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", roomReservation.RoomId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", roomReservation.StudentId);
            return View(roomReservation);
        }

        // GET: RoomReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomReservation = await _context.RoomReservation
                .Include(r => r.Room)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomReservation == null)
            {
                return NotFound();
            }

            return View(roomReservation);
        }

        // POST: RoomReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomReservation = await _context.RoomReservation.FindAsync(id);
            _context.RoomReservation.Remove(roomReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomReservationExists(int id)
        {
            return _context.RoomReservation.Any(e => e.Id == id);
        }
    }
}
