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
    public class EftTransactionQueryHandler :
      IRequestHandler<GetAllEftTransactionQuery, ApiResponse<List<EftTransactionResponse>>>,
      IRequestHandler<GetEftTransactionByIdQuery, ApiResponse<EftTransactionResponse>>,
      IRequestHandler<GetEftTransactionByParameterQuery, ApiResponse<List<EftTransactionResponse>>>
    {
        private readonly AkbDbContext dbContext;
        private readonly IMapper mapper;

        public EftTransactionQueryHandler(AkbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetAllEftTransactionQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<EftTransaction>()
                .Include(x => x.Account)
                .ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<EftTransaction>, List<EftTransactionResponse>>(list);
            return new ApiResponse<List<EftTransactionResponse>>(mappedList);
        }

        public async Task<ApiResponse<EftTransactionResponse>> Handle(GetEftTransactionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<EftTransaction>()
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return new ApiResponse<EftTransactionResponse>("Record not found");
            }

            var mapped = mapper.Map<EftTransaction, EftTransactionResponse>(entity);
            return new ApiResponse<EftTransactionResponse>(mapped);
        }

        public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetEftTransactionByParameterQuery request,
            CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<EftTransaction>()
                .Include(x => x.Account)
                .Where(x =>
                (string.IsNullOrEmpty(request.ReferenceNumber) || x.ReferenceNumber.ToUpper().Contains(request.ReferenceNumber.ToUpper())) &&
                 (string.IsNullOrEmpty(request.SenderName) || x.SenderName.ToUpper().Contains(request.SenderName.ToUpper()))
            ).ToListAsync(cancellationToken);
            if (list.Count() == 0)
            {
                return new ApiResponse<List<EftTransactionResponse>>("Record not found");
            }
            var mappedList = mapper.Map<List<EftTransaction>, List<EftTransactionResponse>>(list);
            return new ApiResponse<List<EftTransactionResponse>>(mappedList);
        }
    }
}
