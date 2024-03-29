﻿using Akb.Base.Response;
using Akb.Business.Cqrs;
using Akb.Data;
using Akb.Data.Entity;
using Akb.Schema;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Akb.Business.Command
{
    public class AccountCommandHandler :
     IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
     IRequestHandler<UpdateAccountCommand, ApiResponse>,
     IRequestHandler<DeleteAccountCommand, ApiResponse>

    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public AccountCommandHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            var entity = mapper.Map<AccountRequest, Account>(request.Model);


            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Account, AccountResponse>(entityResult.Entity);
            return new ApiResponse<AccountResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Account>().Where(x => x.AccountNumber == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.Balance = request.Model.Balance;
            fromdb.Name = request.Model.Name;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Account>().Where(x => x.AccountNumber == request.Id)
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
