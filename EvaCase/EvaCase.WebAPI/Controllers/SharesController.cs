using EvaCase.Application.Features.Shares.GetAllShares;
using EvaCase.Application.Features.Shares.RegisterShare;
using EvaCase.Application.Features.Shares.UpdateSharePrice;
using EvaCase.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaCase.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class SharesController : ApiController
    {
        public SharesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterShareCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePrice(UpdateSharePriceCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllSharesQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
