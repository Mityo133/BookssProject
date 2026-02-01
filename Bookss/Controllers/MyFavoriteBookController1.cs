using Bookss.Data;
using Bookss.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookss.Controllers
{
    public class MyFavoriteBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyFavoriteBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyFavoriteBook
        public async Task<IActionResult> Index()
        {
            return View(await _context.MyFavoriteBooks.ToListAsync());
        }

        // GET: MyFavoriteBook/Create
        public IActionResult Create()
        {
            var books = _context.Books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
            var users = _context.Users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.UserName });
            ViewData["Books"] = books;
            ViewData["Users"] = users;
            return View();
        }

        // POST: MyFavoriteBook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BookId")] MyFavoriteBook myFavoriteBook)
        {
            if (ModelState.IsValid)
            {
                _context.MyFavoriteBooks.Add(myFavoriteBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var books = _context.Books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
            var users = _context.Users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.UserName });
            ViewData["Books"] = books;
            ViewData["Users"] = users;
            return View(myFavoriteBook);
        }

        // GET: MyFavoriteBook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myFavoriteBook = await _context.MyFavoriteBooks.FindAsync(id);
            if (myFavoriteBook == null)
            {
                return NotFound();
            }
            var books = _context.Books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
            var users = _context.Users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.UserName });
            ViewData["Books"] = books;
            ViewData["Users"] = users;
            return View(myFavoriteBook);
        }

        // POST: MyFavoriteBook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,BookId")] MyFavoriteBook myFavoriteBook)
        {
            if (id != myFavoriteBook.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myFavoriteBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyFavoriteBookExists(myFavoriteBook.Id))
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
            var books = _context.Books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
            var users = _context.Users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.UserName });
            ViewData["Books"] = books;
            ViewData["Users"] = users;
            return View(myFavoriteBook);
        }

        // GET: MyFavoriteBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myFavoriteBook = await _context.MyFavoriteBooks.FindAsync(id);
            if (myFavoriteBook == null)
            {
                return NotFound();
            }
            return View(myFavoriteBook);
        }

        // POST: MyFavoriteBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myFavoriteBook = await _context.MyFavoriteBooks.FindAsync(id);
            _context.MyFavoriteBooks.Remove(myFavoriteBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyFavoriteBookExists(int id)
        {
            return _context.MyFavoriteBooks.Any(e => e.Id == id);
        }
    }
}