using BookAuthors.Data;
using BookAuthors.Models;

namespace BookAuthors.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author?> GetAuthorWithBooksAsync(int id);
        Task UpdateAuthorBooksAsync(int authorId, IEnumerable<int> bookIds);
    }
    public class AuthorRepository(ApplicationDbContext context) : Repository<Author>(context), IAuthorRepository
    {
        public Task<Author?> GetAuthorWithBooksAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAuthorBooksAsync(int authorId, IEnumerable<int> bookIds)
        {
            throw new NotImplementedException();
        }
    }
}
