using Akb.Schema;
using FluentValidation;

namespace Akb.Business.Validator
{
    public class CreateAccountValidator : AbstractValidator<AccountRequest>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.IBAN).NotEmpty().MaximumLength(34).MinimumLength(34).WithName("IBAN");
            RuleFor(x => x.CurrencyType).NotEmpty().MaximumLength(3).WithName("Currency Type");
        }
    }
}
