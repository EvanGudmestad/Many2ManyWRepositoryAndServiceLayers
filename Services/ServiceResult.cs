using FluentValidation.Results;

namespace BookAuthors.Services
{
    public class ServiceResult
    {
        public bool Success { get; init; }
        public List<ValidationFailure> Errors { get; init; } = [];

        public static ServiceResult Ok() => new() { Success = true };

        public static ServiceResult Fail(IEnumerable<ValidationFailure> errors) =>
            new() { Success = false, Errors = errors.ToList() };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; init; }

        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };

        public static new ServiceResult<T> Fail(IEnumerable<ValidationFailure> errors) =>
            new() { Success = false, Errors = errors.ToList() };
    }
}
