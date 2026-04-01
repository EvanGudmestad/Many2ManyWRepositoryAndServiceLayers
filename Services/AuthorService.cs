using BookAuthors.Models;
using BookAuthors.Repositories;

namespace BookAuthors.Services
{

    public interface IAuthorService
    {
       Task<IEnumerable<Author>> GetAllAuthorsAsync();

        //Task UpdateAuthorBooksAsync(int authorId, IEnumerable<int> bookIds);

    }
    public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }
    }
}
