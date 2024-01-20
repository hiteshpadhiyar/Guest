using FluentValidation;
using Guest.CQRS.Commands;

namespace Guest.CQRS.Validators
{
    public class CreateNewGuestCommandValidator : AbstractValidator<CreateGuestCommand>
    {
        public CreateNewGuestCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{FirstName} required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("{Email} required");
            RuleFor(x => x.Phone_Numbers).NotEmpty();
        }
    }
}
