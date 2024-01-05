using Akb.Base.Response;
using Akb.Schema;
using MediatR;

namespace Akb.Business.Cqrs
{
    public record CreateAccountCommand(AccountRequest Model) : IRequest<ApiResponse<AccountResponse>>;
    public record UpdateAccountCommand(int Id, AccountRequest Model) : IRequest<ApiResponse>;
    public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
    public record GetAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;
    public record GetAccountByParameterQuery(string Name) : IRequest<ApiResponse<List<AccountResponse>>>;
}
