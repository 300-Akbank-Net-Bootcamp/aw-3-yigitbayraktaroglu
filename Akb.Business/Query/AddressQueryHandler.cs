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
    public class AddressQueryHandler :
     IRequestHandler<GetAllAddressQuery, ApiResponse<List<AddressResponse>>>,
     IRequestHandler<GetAddressByIdQuery, ApiResponse<AddressResponse>>
    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public AddressQueryHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAllAddressQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Address>()
                .Include(x => x.Customer).ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<Address>, List<AddressResponse>>(list);
            return new ApiResponse<List<AddressResponse>>(mappedList);
        }

        public async Task<ApiResponse<AddressResponse>> Handle(GetAddressByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Address>()
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<AddressResponse>("Record not found");
            }

            var mapped = mapper.Map<Address, AddressResponse>(entity);
            return new ApiResponse<AddressResponse>(mapped);
        }

        public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAddressByParameterQuery request,
           CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Address>()
                .Include(x => x.Customer)
                .Where(x =>
                (string.IsNullOrEmpty(request.Country) || x.Country.ToUpper().Contains(request.Country.ToUpper())) &&
                 (string.IsNullOrEmpty(request.City) || x.City.ToUpper().Contains(request.City.ToUpper())) &&
                 (string.IsNullOrEmpty(request.Address1) || x.Address1.ToUpper().Contains(request.Address1.ToUpper()))
            ).ToListAsync(cancellationToken);
            if (list.Count == 0)
            {
                return new ApiResponse<List<AddressResponse>>("Record not found");
            }
            var mappedList = mapper.Map<List<Address>, List<AddressResponse>>(list);
            return new ApiResponse<List<AddressResponse>>(mappedList);
        }
    }
}
