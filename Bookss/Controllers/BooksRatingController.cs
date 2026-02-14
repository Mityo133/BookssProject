using Bookss.Data;
using Bookss.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookss.Controllers
{
    public class BooksRatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksRatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BooksRatings
        public async Task<IActionResult> Index()
        {
            return View(await _context.BooksRating.ToListAsync());
        }

        // GET: BooksRatings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BooksRatings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,UserId1,Rating,Text")] BooksRating booksRating)
        {
            ModelState.Remove("Book");
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                _context.BooksRating.Add(booksRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booksRating);
        }

        // GET: BooksRatings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            return View(booksRating);
        }

        // POST: BooksRatings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BookId,UserId1,Rating,Text")] BooksRating booksRating)
        {
            if (id != booksRating.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Book");
            ModelState.Remove("User");
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
            return View(booksRating);
        }

        // GET: BooksRatings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
            return View(booksRating);
        }

        // POST: BooksRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var booksRating = await _context.BooksRating.FindAsync(id);
            _context.BooksRating.Remove(booksRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksRatingExists(Guid id)
        {
            return _context.BooksRating.Any(e => e.Id == id);
        }
    }
}
