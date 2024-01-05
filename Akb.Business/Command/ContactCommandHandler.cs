using Akb.Base.Response;
using Akb.Business.Cqrs;
using Akb.Data;
using Akb.Data.Entity;
using Akb.Schema;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Akb.Business.Command
{
    public class ContactCommandHandler :
     IRequestHandler<CreateContactCommand, ApiResponse<ContactResponse>>,
     IRequestHandler<UpdateContactCommand, ApiResponse>,
     IRequestHandler<DeleteContactCommand, ApiResponse>

    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public ContactCommandHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ContactResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {

            var entity = mapper.Map<ContactRequest, Contact>(request.Model);


            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Contact, ContactResponse>(entityResult.Entity);
            return new ApiResponse<ContactResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.Information = request.Model.Information;
            fromdb.ContactType = request.Model.ContactType;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }
            //dbContext.Set<Customer>().Remove(fromdb);

            fromdb.IsActive = false;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
