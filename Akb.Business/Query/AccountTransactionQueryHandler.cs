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
    public class AccountTransactionQueryHandler :
      IRequestHandler<GetAllAccountTransactionQuery, ApiResponse<List<AccountTransactionResponse>>>,
      IRequestHandler<GetAccountTransactionByIdQuery, ApiResponse<AccountTransactionResponse>>,
      IRequestHandler<GetAccountTransactionByParameterQuery, ApiResponse<List<AccountTransactionResponse>>>
    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public AccountTransactionQueryHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAllAccountTransactionQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<AccountTransaction>()
                .Include(x => x.Account)
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<AccountTransaction>, List<AccountTransactionResponse>>(list);
            return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
        }

        public async Task<ApiResponse<AccountTransactionResponse>> Handle(GetAccountTransactionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<AccountTransaction>()
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<AccountTransactionResponse>("Record not found");
            }

            var mapped = mapper.Map<AccountTransaction, AccountTransactionResponse>(entity);
            return new ApiResponse<AccountTransactionResponse>(mapped);
        }

        public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAccountTransactionByParameterQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<AccountTransaction>()
                .Include(x => x.Account)
                .Where(x =>
                (string.IsNullOrEmpty(request.ReferenceNumber) || x.ReferenceNumber.ToUpper().Contains(request.ReferenceNumber.ToUpper())) &&
                 (string.IsNullOrEmpty(request.TransferType) || x.TransferType.ToUpper().Contains(request.TransferType.ToUpper()))
            ).ToListAsync(cancellationToken);
            if (list.Count == 0)
            {
                return new ApiResponse<List<AccountTransactionResponse>>("Record not found");
            }
            var mappedList = mapper.Map<List<AccountTransaction>, List<AccountTransactionResponse>>(list);
            return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
        }
    }
}
