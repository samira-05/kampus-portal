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

        public async Task<IActionResult> Index()
        {
            var clubs = await _context.Clubs.ToListAsync();
            return View(clubs);
        }

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
    }
}