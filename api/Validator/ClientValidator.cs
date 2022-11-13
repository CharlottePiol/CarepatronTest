using api.Models;
using FluentValidation;

namespace api.Validator
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(m => m.Id).NotEmpty().WithMessage("ID is required.");
            RuleFor(m => m.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(m => m.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(m => m.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required.");
            RuleFor(m => m.Email).NotEmpty().WithMessage("Email address is required")
                        .EmailAddress().WithMessage("A valid email is required");
        }
    }
}