using Akb.Base.Response;
using Akb.Business.Cqrs;
using Akb.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Akb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EftTransactionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public EftTransactionsController(IMediator mediator)
        {
            this.mediator = mediator;

        }


        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ApiResponse<List<EftTransactionResponse>>> Get()
        {
            var operation = new GetAllEftTransactionQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<EftTransactionResponse>> GetbyId(int id)
        {
            var operation = new GetEftTransactionByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetByParameter")]
        public async Task<ApiResponse<List<EftTransactionResponse>>> GetByParameter(string? ReferenceNumber, string? SenderName)
        {
            var operation = new GetEftTransactionByParameterQuery(ReferenceNumber, SenderName);
            var result = await mediator.Send(operation);
            return result;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ApiResponse<EftTransactionResponse>> Post([FromBody] EftTransactionRequest eftTransaction)
        {
            var operation = new CreateEftTransactionCommand(eftTransaction);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] EftTransactionRequest eftTransaction)
        {
            eftTransaction.Id = id;
            var operation = new UpdateEftTransactionCommand(id, eftTransaction);
            var result = await mediator.Send(operation);
            return result;

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteEftTransactionCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
