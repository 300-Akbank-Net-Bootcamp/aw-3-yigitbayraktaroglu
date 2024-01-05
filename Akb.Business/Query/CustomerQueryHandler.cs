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
    public class CustomerQueryHandler :
      IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>,
      IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>,
      IRequestHandler<GetCustomerByParameterQuery, ApiResponse<List<CustomerResponse>>>
    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public CustomerQueryHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Customer>()
                .Include(x => x.Accounts)
                .Include(x => x.Contacts)
                .Include(x => x.Addresses).ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(list);
            return new ApiResponse<List<CustomerResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Customer>()
                .Include(x => x.Accounts)
                .Include(x => x.Contacts)
                .Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.CustomerNumber == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<CustomerResponse>("Record not found");
            }

            var mapped = mapper.Map<Customer, CustomerResponse>(entity);
            return new ApiResponse<CustomerResponse>(mapped);
        }

        public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByParameterQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Customer>()
                .Include(x => x.Accounts)
                .Include(x => x.Contacts)
                .Include(x => x.Addresses)
                .Where(x =>
                 (string.IsNullOrEmpty(request.FirstName) || x.FirstName.ToUpper().Contains(request.FirstName.ToUpper())) &&
                 (string.IsNullOrEmpty(request.LastName) || x.LastName.ToUpper().Contains(request.LastName.ToUpper())) &&
                 (string.IsNullOrEmpty(request.IdentiyNumber) || x.IdentityNumber.ToUpper().Contains(request.IdentiyNumber.ToUpper()))
            ).ToListAsync(cancellationToken);
            if (list.Count == 0)
            {
                return new ApiResponse<List<CustomerResponse>>("Record not found");
            }
            var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(list);
            return new ApiResponse<List<CustomerResponse>>(mappedList);
        }
    }
}
