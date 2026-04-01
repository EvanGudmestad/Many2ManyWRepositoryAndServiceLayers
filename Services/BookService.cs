using BookAuthors.Infrastructure;
using BookAuthors.Models;
using BookAuthors.Repositories;

namespace BookAuthors.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync();
        Task<ServiceResult<Book>> CreateBookAsync(Book book);

        Task UpdateBookAuthorsAsync(int bookId, IEnumerable<int> authorIds);



    }
    public class BookService(IBookRepository bookRepository) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<ServiceResult<Book>> CreateBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            return ServiceResult<Book>.Ok(book);
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync()
        {
            var options = new QueryOptions<Book>();
            options.AddInclude(b => b.Authors);
            options.OrderBy = b => b.Title;
            return await _bookRepository.GetAllAsync(options);
        }

        public async Task UpdateBookAuthorsAsync(int bookId, IEnumerable<int> authorIds)
        {
            await _bookRepository.UpdateBookAuthorsAsync(bookId, authorIds);
        }

        //public async Task<ServiceResult<Book>> CreateBookAsync(Book book)
        //{

        //}
    }
}
