using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandValidator: AbstractValidator<UpdateStreamerCommand>
{
    public UpdateStreamerCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull().WithMessage("{Name} could not be null");
        
        RuleFor(p => p.Url)
            .NotNull().WithMessage("{Url} could not be null");

    }
}