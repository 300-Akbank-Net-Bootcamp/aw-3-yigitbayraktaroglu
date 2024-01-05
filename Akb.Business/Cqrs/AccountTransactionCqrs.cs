using Akb.Base.Response;
using Akb.Schema;
using MediatR;

namespace Akb.Business.Cqrs
{
    public record CreateAccountTransactionCommand(AccountTransactionRequest Model) : IRequest<ApiResponse<AccountTransactionResponse>>;
    public record UpdateAccountTransactionCommand(int Id, AccountTransactionRequest Model) : IRequest<ApiResponse>;
    public record DeleteAccountTransactionCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllAccountTransactionQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
    public record GetAccountTransactionByIdQuery(int Id) : IRequest<ApiResponse<AccountTransactionResponse>>;
    public record GetAccountTransactionByParameterQuery(string ReferenceNumber, string TransferType) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
}
