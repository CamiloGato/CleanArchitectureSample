using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
{
    public CreateStreamerCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The Name Could not be null")
            .MaximumLength(50).WithMessage("The Name Could not pass the 50 characters");

        RuleFor(p => p.Url)
            .NotEmpty().WithMessage("The Url Could not be null or blank");
    }
}