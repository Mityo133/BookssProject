using Bookss.Data;
using Bookss.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookss.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var authors = _context.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            var genres = _context.Genre.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            ViewData["Authors"] = authors;
            ViewData["Genres"] = genres;
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,GenreId,AutorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var authors = _context.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            var genres = _context.Genre.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            ViewData["Authors"] = authors;
            ViewData["Genres"] = genres;
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var authors = _context.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            var genres = _context.Genre.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            ViewData["Authors"] = authors;
            ViewData["Genres"] = genres;
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,GenreId,AutorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            var authors = _context.Authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            var genres = _context.Genre.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            ViewData["Authors"] = authors;
            ViewData["Genres"] = genres;
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
