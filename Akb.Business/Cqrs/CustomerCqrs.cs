using Akb.Base.Response;
using Akb.Schema;
using MediatR;

namespace Akb.Business.Cqrs
{
    public record CreateCustomerCommand(CustomerRequest Model) : IRequest<ApiResponse<CustomerResponse>>;
    public record UpdateCustomerCommand(int Id, CustomerRequest Model) : IRequest<ApiResponse>;
    public record DeleteCustomerCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
    public record GetCustomerByIdQuery(int Id) : IRequest<ApiResponse<CustomerResponse>>;
    public record GetCustomerByParameterQuery(string FirstName, string LastName, string IdentiyNumber) : IRequest<ApiResponse<List<CustomerResponse>>>;
}
