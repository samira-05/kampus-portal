using KampusPortal.Data;
using KampusPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KampusPortal.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Index
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .Include(e => e.Club)
                .ToListAsync();
            return View(events);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.Events
                .Include(e => e.Club)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null) return NotFound();

            return View(eventItem);
        }

        //Create
        public IActionResult Create()
        {
            ViewBag.Clubs = new SelectList(_context.Clubs, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event eventItem)
        {
            if (eventItem.EventDate < DateTime.Now)
            {
                ModelState.AddModelError("EventDate", "Etkinlik tarihi geçmiş bir tarih olamaz.");
            }

            if (ModelState.IsValid)
            {
                _context.Events.Add(eventItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clubs = new SelectList(_context.Clubs, "Id", "Name", eventItem.ClubId);
            return View(eventItem);
        }

        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();

            ViewBag.Clubs = new SelectList(_context.Clubs, "Id", "Name", eventItem.ClubId);
            return View(eventItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Event eventItem)
        {
            if (id != eventItem.Id) return NotFound();

            if (eventItem.EventDate < DateTime.Now)
            {
                ModelState.AddModelError("EventDate", "Etkinlik tarihi geçmiş bir tarih olamaz.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(eventItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clubs = new SelectList(_context.Clubs, "Id", "Name", eventItem.ClubId);
            return View(eventItem);
        }

        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.Events
                .Include(e => e.Club)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null) return NotFound();

            return View(eventItem);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem != null)
            {
                _context.Events.Remove(eventItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}