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
    public class FavoriteBooksController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoriteBooksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FavoriteBooks
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            return View(await _context.MyFavoriteBooks
                .Include(f => f.Book)
                .Where(f => f.UserId == user.Id)
                .ToListAsync());
        }

        // GET: FavoriteBooks/Create
        public IActionResult Create()
        {
            Load();
            return View();
        }

        // POST: FavoriteBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId")] MyFavoriteBook myFavoriteBook)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            myFavoriteBook.UserId = user.Id;

            ModelState.Remove("User");
            ModelState.Remove("Book");
            ModelState.Remove("UserId"); // Ensure UserId is not validated from binder if it was present

            if (ModelState.IsValid)
            {
                _context.MyFavoriteBooks.Add(myFavoriteBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Load();
            return View(myFavoriteBook);
        }

        // GET: FavoriteBooks/Edit/5
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

            // Ensure user owns this record
            var user = await _userManager.GetUserAsync(User);
            if (user == null || myFavoriteBook.UserId != user.Id)
            {
                return NotFound();
            }

            var books = _context.Books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
            ViewData["Books"] = books;
            return View(myFavoriteBook);
        }

        // POST: FavoriteBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId")] MyFavoriteBook myFavoriteBook)
        {
            if (id != myFavoriteBook.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Retrieve existing to check ownership and set UserId
            var existing = await _context.MyFavoriteBooks.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
            if (existing == null || existing.UserId != user.Id)
            {
                return NotFound();
            }

            myFavoriteBook.UserId = user.Id;

            ModelState.Remove("User");
            ModelState.Remove("Book");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myFavoriteBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteBooksExists(myFavoriteBook.Id))
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
            ViewData["Books"] = books;
            return View(myFavoriteBook);
        }

        // GET: FavoriteBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myFavoriteBook = await _context.MyFavoriteBooks
                .Include(f => f.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myFavoriteBook == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || myFavoriteBook.UserId != user.Id)
            {
                return NotFound();
            }

            return View(myFavoriteBook);
        }

        // POST: FavoriteBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myFavoriteBook = await _context.MyFavoriteBooks.FindAsync(id);

            var user = await _userManager.GetUserAsync(User);
            if (user == null || (myFavoriteBook != null && myFavoriteBook.UserId != user.Id))
            {
                return NotFound();
            }

            if (myFavoriteBook != null)
            {
                _context.MyFavoriteBooks.Remove(myFavoriteBook);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteBooksExists(int id)
        {
            return _context.MyFavoriteBooks.Any(e => e.Id == id);
        }
        private void Load()
        {
            ViewBag.BookId = _context.Books.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title }).ToList();

        }
    }
}