using EvaCase.Application.Features.Trades.ExecuteTrade;
using EvaCase.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaCase.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class TradesController : ApiController
    {
        public TradesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Buy(ExecuteTradeCommand request, CancellationToken cancellationToken)
        {
            var command = request with { Type = "BUY" };
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Sell(ExecuteTradeCommand request, CancellationToken cancellationToken)
        {
            var command = request with { Type = "SELL" };
            var response = await _mediator.Send(command, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
