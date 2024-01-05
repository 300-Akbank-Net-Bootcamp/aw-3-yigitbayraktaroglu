using Akb.Data.Entity;
using MediatR;

namespace Akb.Business
{
    public record AkbTransferCommand : IRequest<Customer>
    {
    }

}
