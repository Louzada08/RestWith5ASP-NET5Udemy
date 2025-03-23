using Asp.Versioning;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Application.Commands.Persons.Requests;
using RestWithASPNET.Application.Commands.Persons.Responses;
using RestWithASPNET.Application.Services.Persons;
using RestWithASPNET.Domain.Filter;
using RestWithASPNET.Domain.Validation;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.WebApi.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : MainController
    {
        private readonly IPersonService _personService;

        public PersonController(IMediator mediator, IPersonService personService, IMapper mapper) :
                base(mapper, mediator)
        {
            _personService = personService;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllPerson()
        {
            return Ok(await _personService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetPersonById(Guid id)
        {
            try
            {
                var personResp = _mapper.Map<CreatePersonResponse>(await _personService.FindById(id));

                if (personResp is not null) return CustomResponse(personResp);

                var bag = new ValidationResultBag();
                bag.Errors.Add(new ValidationFailure("error", "Pessoa não encontrada", StatusCodes.Status404NotFound));
                return CustomResponse(bag);
            }
            catch (Exception ex)
            {
                var bag = new ValidationResultBag();
                bag.Errors.Add(new ValidationFailure("error", ex.Message));
                return CustomResponse(bag);
            }

            var person = await _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            //return Ok(await _personService.FindWithPagedSearch(name, sortDirection, pageSize, page));
            return Ok("Não implementado");
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get([FromQuery] PersonFilter filter)
        {
            var person = await _personService.FindByName(filter);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePersonRequest command)
        {
            var response = await _mediator.Send(command);
            return CustomResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(await _personService.Update(person));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        public async Task<IActionResult> Patch(Guid id)
        {
            //var person = await _personService.Disable(id);
            return Ok();
        }
    }
}
