using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionPlatform.Data;
using PetAdoptionPlatform.Models;

namespace PetAdoptionPlatform.Controllers
{
    public class AdoptionRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdoptionRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdoptionRequests.Include(a => a.Animal).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdoptionRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionRequest = await _context.AdoptionRequests
                .Include(a => a.Animal)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionRequest == null)
            {
                return NotFound();
            }

            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AdoptionRequests/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AnimalId,RequestDate,Status")] AdoptionRequest adoptionRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoptionRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", adoptionRequest.AnimalId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", adoptionRequest.UserId);
            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionRequest = await _context.AdoptionRequests.FindAsync(id);
            if (adoptionRequest == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", adoptionRequest.AnimalId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", adoptionRequest.UserId);
            return View(adoptionRequest);
        }

        // POST: AdoptionRequests/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AnimalId,RequestDate,Status")] AdoptionRequest adoptionRequest)
        {
            if (id != adoptionRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptionRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionRequestExists(adoptionRequest.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", adoptionRequest.AnimalId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", adoptionRequest.UserId);
            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionRequest = await _context.AdoptionRequests
                .Include(a => a.Animal)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionRequest == null)
            {
                return NotFound();
            }

            return View(adoptionRequest);
        }

        // POST: AdoptionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoptionRequest = await _context.AdoptionRequests.FindAsync(id);
            if (adoptionRequest != null)
            {
                _context.AdoptionRequests.Remove(adoptionRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionRequestExists(int id)
        {
            return _context.AdoptionRequests.Any(e => e.Id == id);
        }
    }
}
