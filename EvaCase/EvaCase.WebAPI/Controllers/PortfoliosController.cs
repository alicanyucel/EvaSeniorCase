using EvaCase.Application.Features.Portfolios.GetAllPortfolios;
using EvaCase.Application.Features.Portfolios.RegisterPortfolio;
using EvaCase.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaCase.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class PortfoliosController : ApiController
    {
        public PortfoliosController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterPortfolioCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllPortfoliosQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
