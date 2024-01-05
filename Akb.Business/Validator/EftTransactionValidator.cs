using Akb.Schema;
using FluentValidation;

namespace Akb.Business.Validator
{
    public class CreateEftTransactionValidator : AbstractValidator<EftTransactionRequest>
    {
        public CreateEftTransactionValidator()
        {
            RuleFor(x => x.SenderIban).NotEmpty().MaximumLength(50).WithName("Sender IBAN");
            RuleFor(x => x.SenderName).NotEmpty().MaximumLength(50).WithName("Sender Name");
            RuleFor(x => x.ReferenceNumber).NotEmpty().MaximumLength(50).WithName("Reference Number");
        }
    }
}
