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
    }
}
