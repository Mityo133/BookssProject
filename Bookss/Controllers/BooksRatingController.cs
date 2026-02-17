using Bookss.Data;
using Bookss.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookss.Controllers
{
    [Authorize]
    public class BooksRatingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BooksRatingController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BooksRatings
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            return View(await _context.BooksRating
                .Include(b => b.Book)
                .Where(r => r.UserId == user.Id)
                .ToListAsync());
        }
        // GET: BooksRatings/Create
        public IActionResult Create()
        {
            Load();
            return View();
        }

        // POST: BooksRatings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,Rating,Text")] BooksRating booksRating)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            booksRating.UserId = user.Id;

            ModelState.Remove("Book");
            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                _context.BooksRating.Add(booksRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Load();
            return View(booksRating);
        }

        // GET: BooksRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksRating = await _context.BooksRating.FindAsync(id);
            if (booksRating == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || booksRating.UserId != user.Id)
            {
                return NotFound();
            }

            Load();
            return View(booksRating);
        }

        // POST: BooksRatings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,Rating,Text")] BooksRating booksRating)
        {
            if (id != booksRating.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var existing = await _context.BooksRating.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (existing == null || existing.UserId != user.Id)
            {
                return NotFound();
            }

            booksRating.UserId = user.Id;

            ModelState.Remove("Book");
            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booksRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksRatingExists(booksRating.Id))
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
            Load();
            return View(booksRating);
        }

        // GET: BooksRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booksRating = await _context.BooksRating
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booksRating == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || booksRating.UserId != user.Id)
            {
                return NotFound();
            }

            return View(booksRating);
        }

        // POST: BooksRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booksRating = await _context.BooksRating.FindAsync(id);

            var user = await _userManager.GetUserAsync(User);
            if (user == null || (booksRating != null && booksRating.UserId != user.Id))
            {
                return NotFound();
            }

            if (booksRating != null)
            {
                _context.BooksRating.Remove(booksRating);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BooksRatingExists(int id)
        {
            return _context.BooksRating.Any(e => e.Id == id);
        }

        private void Load()
        {
            ViewBag.BookId = _context.Books.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Title
            }).ToList();
        }
    }
}
