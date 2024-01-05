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
    public class ContactsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;

        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ApiResponse<List<ContactResponse>>> Get()
        {
            var operation = new GetAllContactQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse<ContactResponse>> GetbyId(int id)
        {
            var operation = new GetContactByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;


        }

        [HttpGet("GetByParameter")]
        public async Task<ApiResponse<List<ContactResponse>>> GetByParameter(string? Information, string? ContactType)
        {
            var operation = new GetContactByParameterQuery(ContactType, Information);
            var result = await mediator.Send(operation);
            return result;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ApiResponse<ContactResponse>> Post([FromBody] ContactRequest contact)
        {
            var operation = new CreateContactCommand(contact);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] ContactRequest contact)
        {
            contact.Id = id;
            var operation = new UpdateContactCommand(id, contact);
            var result = await mediator.Send(operation);
            return result;

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteContactCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
