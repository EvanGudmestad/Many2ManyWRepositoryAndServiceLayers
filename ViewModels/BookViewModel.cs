using BookAuthors.Models;

namespace BookAuthors.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<int> SelectedAuthorIds { get; set; } = [];

        public List<Author> AvailableAuthors { get; set; } = [];

        public List<Author> CurrentAuthors { get; set; } = [];


    }
}
