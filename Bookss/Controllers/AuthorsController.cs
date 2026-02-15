using Books.Models.ViewModels.Authors;
using Books.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookss.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        // Вече инжектираме само сървиса
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return View(authors); // Връща IEnumerable<AuthorViewModel>
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateOrEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _authorService.CreateAuthorAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _authorService.GetAuthorByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _authorService.GetAuthorForEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AuthorCreateOrEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _authorService.UpdateAuthorAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _authorService.GetAuthorByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _authorService.DeleteAuthorAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}