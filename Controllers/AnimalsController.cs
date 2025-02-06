using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PetAdoptionPlatform.Data;
using PetAdoptionPlatform.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetAdoptionPlatform.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AnimalsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var animals = await _context.Animals
                .Include(a => a.AddedByUser)  
                .ToListAsync();

            return View(animals);
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();

            return View(animal);
        }

        // GET: Animals/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Species,Breed,Age,Description,Status,ImageUrl")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    if (int.TryParse(user.Id.ToString(), out int parsedUserId))
                    {
                        animal.AddedByUserId = parsedUserId;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ошибка преобразования User ID.");
                        return View(animal);
                    }


                    animal.AddedByUser = user;  // Добавляем объект пользователя
                }

                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }

        // GET: Animals/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();

            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Species,Breed,Age,Description,Status,ImageUrl")] Animal animal)
        {
            if (id != animal.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }

        // GET: Animals/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
