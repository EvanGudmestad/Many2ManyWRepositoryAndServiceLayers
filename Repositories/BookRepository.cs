using BookAuthors.Data;
using BookAuthors.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAuthors.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> GetBookWithAuthorsAsync(int id);
        Task UpdateBookAuthorsAsync(int bookId, IEnumerable<int> authorIds);


    }
    public class BookRepository(ApplicationDbContext context) : Repository<Book>(context), IBookRepository
    {
        public async Task<Book?> GetBookWithAuthorsAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }
        public async Task UpdateBookAuthorsAsync(int bookId, IEnumerable<int> authorIds)
        {
            var book = await GetBookWithAuthorsAsync(bookId);
            if (book == null) return;

            //Remove all book authors
            book.Authors.Clear();


            //Gets all the authors with the selected ids in one query
            var authors = await _context.Set<Author>()
                .Where(a => authorIds.Contains(a.AuthorId))
                .ToListAsync();

            //Add the authors to the book
            foreach (var author in authors)
            {
                book.Authors.Add(author);
            }

            await SaveAsync();
        }
    }
}
