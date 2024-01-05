using Akb.Base.Response;
using Akb.Schema;
using MediatR;

namespace Akb.Business.Cqrs
{

    public record CreateAddressCommand(AddressRequest Model) : IRequest<ApiResponse<AddressResponse>>;
    public record UpdateAddressCommand(int Id, AddressRequest Model) : IRequest<ApiResponse>;
    public record DeleteAddressCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllAddressQuery() : IRequest<ApiResponse<List<AddressResponse>>>;
    public record GetAddressByIdQuery(int Id) : IRequest<ApiResponse<AddressResponse>>;
    public record GetAddressByParameterQuery(string Country, string City, string Address1) : IRequest<ApiResponse<List<AddressResponse>>>;
}
