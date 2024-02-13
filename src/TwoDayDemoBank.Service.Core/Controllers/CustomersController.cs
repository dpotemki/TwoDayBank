﻿using System;
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
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto dto, CancellationToken cancellationToken = default)
        {
            if (null == dto)
                return BadRequest();
            var command = new CreateCustomer(Guid.NewGuid(), dto.FirstName, dto.LastName, dto.Email);
            await _mediator.Send(command, cancellationToken);
            
            return CreatedAtAction("GetCustomer", new { id = command.CustomerId }, command);
        }

        [HttpGet, Route("{id:guid}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(Guid id, CancellationToken cancellationToken= default)
        {
            var query = new CustomerById(id);
            var result = await _mediator.Send(query, cancellationToken);
            if (null == result) 
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var query = new CustomersArchive();
            var results = await _mediator.Send(query, cancellationToken);
            if (null == results)
                return NotFound();
            return Ok(results);
        }
    }
}