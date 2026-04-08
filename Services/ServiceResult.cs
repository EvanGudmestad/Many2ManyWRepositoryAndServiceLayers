using FluentValidation.Results;

namespace BookAuthors.Services
{
    //Result Pattern	Instead of throwing exceptions, return an object that says "it worked" or "it failed with these reasons"
    //The ServiceResult pattern lets your controller handle success/failure uniformly without try-catch blocks everywhere.

    /// <summary>
    /// A wrapper class that represents the outcome of a service operation.
    /// Instead of throwing exceptions or returning null, services return this object
    /// to indicate whether the operation succeeded or failed, along with any errors.
    /// This pattern makes error handling cleaner and more predictable.
    /// </summary>

    public class ServiceResult
    {
        public bool Success { get; init; }//init accessor	Properties can only be set during object creation (immutable after that)
        public List<ValidationFailure> Errors { get; init; } = [];

        //Factory methods	Ok() and Fail() are static methods that create instances for you with the right settings

        public static ServiceResult Ok() => new() { Success = true };

        public static ServiceResult Fail(IEnumerable<ValidationFailure> errors) =>
            new() { Success = false, Errors = errors.ToList() };
    }

    //Generics (<T>)	Allows the class to work with any data type (Book, Author, etc.)

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; init; }

        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };

        public static new ServiceResult<T> Fail(IEnumerable<ValidationFailure> errors) =>
            new() { Success = false, Errors = errors.ToList() };
    }
}
