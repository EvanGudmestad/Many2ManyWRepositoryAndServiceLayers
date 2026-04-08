using BookAuthors.Models;
using FluentValidation;

namespace BookAuthors.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
               .NotEmpty().WithMessage("Title is required.")
               .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
        }
    }
}
