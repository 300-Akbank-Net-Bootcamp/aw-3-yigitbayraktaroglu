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
    public class AccountTransactionCommandHandler :
     IRequestHandler<CreateAccountTransactionCommand, ApiResponse<AccountTransactionResponse>>,
     IRequestHandler<UpdateAccountTransactionCommand, ApiResponse>,
     IRequestHandler<DeleteAccountTransactionCommand, ApiResponse>

    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public AccountTransactionCommandHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<AccountTransactionResponse>> Handle(CreateAccountTransactionCommand request, CancellationToken cancellationToken)
        {

            var entity = mapper.Map<AccountTransactionRequest, AccountTransaction>(request.Model);


            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<AccountTransaction, AccountTransactionResponse>(entityResult.Entity);
            return new ApiResponse<AccountTransactionResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdateAccountTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.Id == request.Id)
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

        public async Task<ApiResponse> Handle(DeleteAccountTransactionCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.Id == request.Id)
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
