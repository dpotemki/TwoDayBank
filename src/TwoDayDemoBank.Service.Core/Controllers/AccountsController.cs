using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwoDayDemoBank.Domain;
using TwoDayDemoBank.Domain.Commands;
using TwoDayDemoBank.Service.Core.Common.Queries;
using TwoDayDemoBank.Service.Core.DTOs;

namespace TwoDayDemoBank.Service.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("{id:guid}", Name = "GetAccount")]
        public async Task<IActionResult> GetAccount(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new AccountById(id);
            var result = await _mediator.Send(query, cancellationToken);
            if (null == result)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto dto, CancellationToken cancellationToken = default)
        {
            if (null == dto)
                return BadRequest();

            var currency = Currency.FromCode(dto.CurrencyCode);
            var command = new CreateAccount(dto.CustomerId, Guid.NewGuid(), currency);
            await _mediator.Send(command, cancellationToken);
            return CreatedAtAction("GetAccount", "Accounts", new { id = command.AccountId }, command);
        }


        [HttpPut, Route("{id:guid}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute]Guid id, [FromBody]DepositDto dto, CancellationToken cancellationToken = default)
        {
            if (null == dto)
                return BadRequest();

            var currency = Currency.FromCode(dto.CurrencyCode);
            var amount = new Money(currency, dto.Amount);
            var command = new Deposit(id, amount);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut, Route("{id:guid}/withdraw")]
        public async Task<IActionResult> Withdraw([FromRoute]Guid id, [FromBody]WithdrawDto dto, CancellationToken cancellationToken = default)
        {
            if (null == dto)
                return BadRequest();

            var currency = Currency.FromCode(dto.CurrencyCode);
            var amount = new Money(currency, dto.Amount);
            var command = new Withdraw(id, amount);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}