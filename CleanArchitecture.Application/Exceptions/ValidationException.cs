using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions;

public class ValidationException()
    : ApplicationException("Validation errors")
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(
            p => p.PropertyName,
            q => q.ErrorMessage
            ).ToDictionary(
                failuresGroup => failuresGroup.Key,
                failureGroup => failureGroup.ToArray()
            );
    }
    
}