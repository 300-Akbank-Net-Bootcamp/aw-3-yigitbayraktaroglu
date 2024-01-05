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
    public class AccountTransactionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountTransactionsController(IMediator mediator)
        {
            this.mediator = mediator;

        }


        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> Get()
        {
            var operation = new GetAllAccountTransactionQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<AccountTransactionResponse>> GetbyId(int id)
        {
            var operation = new GetAccountTransactionByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpGet("GetByParameter")]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> GetbyParameter(string? ReferenceNumber, string? TransferType)
        {
            var operation = new GetAccountTransactionByParameterQuery(ReferenceNumber, TransferType);
            var result = await mediator.Send(operation);
            return result;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ApiResponse<AccountTransactionResponse>> Post([FromBody] AccountTransactionRequest accountTransaction)
        {
            var operation = new CreateAccountTransactionCommand(accountTransaction);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] AccountTransactionRequest accountTransaction)
        {
            accountTransaction.Id = id;
            var operation = new UpdateAccountTransactionCommand(id, accountTransaction);
            var result = await mediator.Send(operation);
            return result;

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAccountTransactionCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
