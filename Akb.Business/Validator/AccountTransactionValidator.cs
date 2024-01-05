using Akb.Schema;
using FluentValidation;

namespace Akb.Business.Validator
{
    public class CreateAccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
    {
        public CreateAccountTransactionValidator()
        {
            RuleFor(x => x.TransferType).NotEmpty().MaximumLength(10).WithName("Transfer Type");
            RuleFor(x => x.ReferenceNumber).NotEmpty().MaximumLength(50).WithName("Reference Number");
        }
    }
}
