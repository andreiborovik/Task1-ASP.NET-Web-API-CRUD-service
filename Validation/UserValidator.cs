using FluentValidation;
using Task1.Models;

namespace Task1.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Surname).NotEmpty();
        }
    }
}
