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
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator;

        }


        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ApiResponse<List<AccountResponse>>> Get()
        {
            var operation = new GetAllAccountQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<AccountResponse>> GetbyId(int id)
        {
            var operation = new GetAccountByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetByParameter")]
        public async Task<ApiResponse<List<AccountResponse>>> GetbyParameter([FromQuery] string? Name)
        {
            var operation = new GetAccountByParameterQuery(Name);
            var result = await mediator.Send(operation);
            return result;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest account)
        {
            var operation = new CreateAccountCommand(account);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] AccountRequest account)
        {
            account.AccountNumber = id;
            var operation = new UpdateAccountCommand(id, account);
            var result = await mediator.Send(operation);
            return result;

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAccountCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
