using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Domain.Validation;

namespace RestWithASPNET.WebApi.Controllers;

[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();
    protected readonly IMediator _mediator;
    protected readonly IMapper _mapper;

    public MainController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
            return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            {"Mensagens", Erros.ToArray() }
        }));
    }

    protected ActionResult CustomResponse(ValidationResultBag validationResultBag)
    {
        if (validationResultBag.Errors.Count > 0)
        {
            var erros = validationResultBag.Errors.Select(x => x.ErrorMessage).ToList();
            foreach (var erro in erros)
                AdicionarErroProcessamento(erro);
        }
        return CustomResponse(validationResultBag.Data);
    }

    protected bool OperacaoValida()
    {
        return !Erros.Any();
    }

    protected void AdicionarErroProcessamento(string erro)
    {
        Erros.Add(erro);
    }

    protected void LimparErrosProcessamento()
    {
        Erros.Clear();
    }
}