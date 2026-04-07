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

        Task<Book?> GetBookByIdAsync(int id);

        Task<Book?> GetBookWithAuthorsAsync(int id);

        Task<ServiceResult> UpdateBookAsync(Book book);

        Task DeleteBookAsync(int id);





    }
    public class BookService(IBookRepository bookRepository) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<ServiceResult<Book>> CreateBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            return ServiceResult<Book>.Ok(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithAuthorsAsync()
        {
            var options = new QueryOptions<Book>();
            options.AddInclude(b => b.Authors);
            options.OrderBy = b => b.Title;
            return await _bookRepository.GetAllAsync(options);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book?> GetBookWithAuthorsAsync(int id)
        {
           return await _bookRepository.GetBookWithAuthorsAsync(id);
        }

        public async Task<ServiceResult> UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
            return ServiceResult.Ok();
        }

        public async Task UpdateBookAuthorsAsync(int bookId, IEnumerable<int> authorIds)
        {

           await _bookRepository.UpdateBookAuthorsAsync(bookId, authorIds);

        }

        Task IBookService.UpdateBookAuthorsAsync(int bookId, IEnumerable<int> authorIds)
        {
            return UpdateBookAuthorsAsync(bookId, authorIds);
        }

        //public async Task<ServiceResult<Book>> CreateBookAsync(Book book)
        //{

        //}
    }
}
