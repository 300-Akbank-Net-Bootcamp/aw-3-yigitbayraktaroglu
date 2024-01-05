using Akb.Base.Response;
using Akb.Business.Cqrs;
using Akb.Data;
using Akb.Data.Entity;
using Akb.Schema;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Akb.Business.Query
{
    public class ContactQueryHandler :
      IRequestHandler<GetAllContactQuery, ApiResponse<List<ContactResponse>>>,
      IRequestHandler<GetContactByIdQuery, ApiResponse<ContactResponse>>,
      IRequestHandler<GetContactByParameterQuery, ApiResponse<List<ContactResponse>>>
    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public ContactQueryHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ContactResponse>>> Handle(GetAllContactQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Contact>()
                .Include(x => x.Customer)
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<Contact>, List<ContactResponse>>(list);
            return new ApiResponse<List<ContactResponse>>(mappedList);
        }

        public async Task<ApiResponse<ContactResponse>> Handle(GetContactByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Contact>()
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<ContactResponse>("Record not found");
            }

            var mapped = mapper.Map<Contact, ContactResponse>(entity);
            return new ApiResponse<ContactResponse>(mapped);
        }

        public async Task<ApiResponse<List<ContactResponse>>> Handle(GetContactByParameterQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Contact>()
                .Include(x => x.Customer)
                .Where(x =>
               (string.IsNullOrEmpty(request.Information) || x.Information.ToUpper().Contains(request.Information.ToUpper())) &&
                 (string.IsNullOrEmpty(request.ContactType) || x.ContactType.ToUpper().Contains(request.ContactType.ToUpper()))
            ).ToListAsync(cancellationToken);
            if (list.Count() == 0)
            {
                return new ApiResponse<List<ContactResponse>>("Record not found");
            }

            var mappedList = mapper.Map<List<Contact>, List<ContactResponse>>(list);
            return new ApiResponse<List<ContactResponse>>(mappedList);
        }
    }
}
