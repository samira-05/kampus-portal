using Microsoft.AspNetCore.Mvc.Rendering;
using KampusPortal.Models;
using KampusPortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KampusPortal.Controllers
{
    public class ClubsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Index
        public async Task<IActionResult> Index(string searchString, ClubCategory? categoryFilter)
        {
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(ClubCategory)), categoryFilter);
            ViewBag.CurrentSearch = searchString;

            var clubsQuery = _context.Clubs.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                clubsQuery = clubsQuery.Where(c => c.Name.ToLower().Contains(searchString.ToLower()));
            }

            if (categoryFilter.HasValue)
            {
                clubsQuery = clubsQuery.Where(c => c.Category == categoryFilter.Value);
            }

            var clubs = await clubsQuery.ToListAsync();
            return View(clubs);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .Include(c => c.Events)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
            {
                return NotFound();
            }
            return View(club);
        }
        //Create new club
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Club club)
        {
            if(ModelState.IsValid)
            {
                _context.Clubs.Add(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        //Edit 
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var club = await _context.Clubs.FindAsync(id);
            if (club == null) return NotFound();

            return View(club);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Club club)
        {
            if (id != club.Id) return NotFound();

            if(ModelState.IsValid)
            {
                _context.Update(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        //Delete
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            if (club == null) return NotFound();

            return View(club);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if(club != null)
            {
                _context.Clubs.Remove(club);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}