using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionPlatform.Data;
using PetAdoptionPlatform.Models;

namespace PetAdoptionPlatform.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var newsList = await _context.News.OrderByDescending(n => n.CreatedAt).ToListAsync();
            ViewBag.IsAdmin = IsAdmin();
            return View(newsList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();

            ViewBag.IsAdmin = IsAdmin();
            return View(news);
        }

        [Authorize]
        public IActionResult Create()
        {
            if (!IsAdmin()) return Forbid();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Title,Content")] News news)
        {
            if (!IsAdmin()) return Forbid();

            news.CreatedByUserId = GetCurrentUserId();
            news.CreatedAt = DateTime.UtcNow;

            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            if (!IsAdmin()) return Forbid();

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] News news)
        {
            if (!IsAdmin()) return Forbid();

            var existingNews = await _context.News.FindAsync(id);
            if (existingNews == null) return NotFound();

            existingNews.Title = news.Title;
            existingNews.Content = news.Content;
            existingNews.CreatedAt = DateTime.UtcNow;

            _context.News.Update(existingNews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            if (!IsAdmin()) return Forbid();

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            if (!IsAdmin()) return Forbid();

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsAdmin()
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserName == User.Identity.Name);
            return user?.Role?.Name == "Admin";
        }

        private int GetCurrentUserId()
        {
            return _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id ?? 0;
        }
    }
}
