using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Services;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personService.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var person = await _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(await _personService.Create(person));
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
        public async Task<IActionResult> Patch(long id)
        {
            var person = await _personService.Disable(id);
            return Ok(person);
        }


    }
}
