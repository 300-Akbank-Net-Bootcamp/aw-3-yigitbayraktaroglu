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
    public class AddressCommandHandler :
     IRequestHandler<CreateAddressCommand, ApiResponse<AddressResponse>>,
     IRequestHandler<UpdateAddressCommand, ApiResponse>,
     IRequestHandler<DeleteAddressCommand, ApiResponse>

    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public AddressCommandHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            //var checkIdentity = await dbContext.Set<Customer>().Where(x => x.IdentityNumber == request.Model.IdentityNumber)
            //    .FirstOrDefaultAsync(cancellationToken);
            //if (checkIdentity != null)
            //{
            //    return new ApiResponse<CustomerResponse>($"{request.Model.IdentityNumber} is used by another customer.");
            //}

            var entity = mapper.Map<AddressRequest, Address>(request.Model);


            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Address, AddressResponse>(entityResult.Entity);
            return new ApiResponse<AddressResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Address>().Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.Address1 = request.Model.Address1;
            fromdb.Address2 = request.Model.Address2;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Address>().Where(x => x.Id == request.Id)
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
