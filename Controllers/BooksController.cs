using BookAuthors.Models;
using BookAuthors.Services;
using BookAuthors.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookAuthors.Controllers
{
    public class BooksController(IBookService bookService, IAuthorService authorService) : Controller
    {
        private readonly IBookService _bookService = bookService;
        private readonly IAuthorService _authorService = authorService;
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksWithAuthorsAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allAuthors = await _authorService.GetAllAuthorsAsync();

            var viewModel = new BookViewModel
            {
                AvailableAuthors = allAuthors.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel viewModel)
        {
            var book = new Book { Title = viewModel.Title };
            var result = await _bookService.CreateBookAsync(book);

            if (viewModel.SelectedAuthorIds.Count > 0)
            {
                await _bookService.UpdateBookAuthorsAsync(result.Data!.BookId, viewModel.SelectedAuthorIds);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookWithAuthorsAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var allAuthors = await _authorService.GetAllAuthorsAsync();
            var viewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                AvailableAuthors = allAuthors.ToList(),
                SelectedAuthorIds = book.Authors.Select(a => a.AuthorId).ToList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel viewModel)
        {
            if (id != viewModel.BookId)
            {
                return NotFound();
            }
            var book = new Book { BookId = viewModel.BookId, Title = viewModel.Title };

            var result = await _bookService.UpdateBookAsync(book);

            await _bookService.UpdateBookAuthorsAsync(id, viewModel.SelectedAuthorIds);

            return RedirectToAction(nameof(Index));


        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookWithAuthorsAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookWithAuthorsAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var allAuthors = await _authorService.GetAllAuthorsAsync();
            var viewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                CurrentAuthors = book.Authors.ToList(),
                AvailableAuthors = allAuthors.ToList(),
                SelectedAuthorIds = book.Authors.Select(a => a.AuthorId).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAuthors(int id, List<int> selectedAuthorIds)
        {
            await _bookService.UpdateBookAuthorsAsync(id, selectedAuthorIds);
            return RedirectToAction(nameof(Details), new { id });
        }

    }
}
