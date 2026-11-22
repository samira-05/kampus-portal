using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KampusPortal.Data;
using KampusPortal.Models;

namespace KampusPortal.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var featuredClubs = await _context.Clubs
     .OrderBy(c => c.Id)
     .Take(3)
     .ToListAsync();

        var upcomingEvents = await _context.Events
            .Include(e => e.Club)
            .Where(e => e.EventDate >= DateTime.Today)
            .OrderBy(e => e.EventDate)
            .Take(4)
            .ToListAsync();

        ViewBag.FeaturedClubs = featuredClubs;
        return View(upcomingEvents);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
