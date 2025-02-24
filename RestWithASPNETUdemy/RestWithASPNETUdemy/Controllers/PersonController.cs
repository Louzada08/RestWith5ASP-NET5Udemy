using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Services;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
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
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(await _personService.Create(person));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(await _personService.Update(person));
        }

    }
}
