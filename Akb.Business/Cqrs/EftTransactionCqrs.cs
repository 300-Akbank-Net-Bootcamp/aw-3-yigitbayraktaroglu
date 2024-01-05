﻿using Akb.Base.Response;
using Akb.Schema;
using MediatR;

namespace Akb.Business.Cqrs
{
    public record CreateEftTransactionCommand(EftTransactionRequest Model) : IRequest<ApiResponse<EftTransactionResponse>>;
    public record UpdateEftTransactionCommand(int Id, EftTransactionRequest Model) : IRequest<ApiResponse>;
    public record DeleteEftTransactionCommand(int Id) : IRequest<ApiResponse>;

    public record GetAllEftTransactionQuery() : IRequest<ApiResponse<List<EftTransactionResponse>>>;
    public record GetEftTransactionByIdQuery(int Id) : IRequest<ApiResponse<EftTransactionResponse>>;
    public record GetEftTransactionByParameterQuery(string ReferenceNumber, string SenderName) : IRequest<ApiResponse<List<EftTransactionResponse>>>;
}
