using Akb.Base.Response;
using Akb.Schema;
using MediatR;

namespace Akb.Business.Cqrs
{
    public record CreateContactCommand(ContactRequest Model) : IRequest<ApiResponse<ContactResponse>>;
    public record UpdateContactCommand(int Id, ContactRequest Model) : IRequest<ApiResponse>;
    public record DeleteContactCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllContactQuery() : IRequest<ApiResponse<List<ContactResponse>>>;
    public record GetContactByIdQuery(int Id) : IRequest<ApiResponse<ContactResponse>>;
    public record GetContactByParameterQuery(string ContactType, string Information) : IRequest<ApiResponse<List<ContactResponse>>>;
}
