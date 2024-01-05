using Akb.Schema;
using FluentValidation;

namespace Akb.Business.Validator
{
    public class CreateContactValidator : AbstractValidator<ContactRequest>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.Information).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ContactType).NotEmpty().MaximumLength(10);


        }
    }
}
