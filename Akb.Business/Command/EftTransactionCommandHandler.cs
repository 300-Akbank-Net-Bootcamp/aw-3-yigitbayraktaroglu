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
    public class EftTransactionCommandHandler :
     IRequestHandler<CreateEftTransactionCommand, ApiResponse<EftTransactionResponse>>,
     IRequestHandler<UpdateEftTransactionCommand, ApiResponse>,
     IRequestHandler<DeleteEftTransactionCommand, ApiResponse>

    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public EftTransactionCommandHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<EftTransactionResponse>> Handle(CreateEftTransactionCommand request, CancellationToken cancellationToken)
        {

            var entity = mapper.Map<EftTransactionRequest, EftTransaction>(request.Model);


            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<EftTransaction, EftTransactionResponse>(entityResult.Entity);
            return new ApiResponse<EftTransactionResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdateEftTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<EftTransaction>().Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.ReferenceNumber = request.Model.ReferenceNumber;
            fromdb.Amount = request.Model.Amount;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteEftTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<EftTransaction>().Where(x => x.Id == request.Id)
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
