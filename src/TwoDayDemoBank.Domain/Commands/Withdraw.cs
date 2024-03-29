﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TwoDayDemoBank.Common;
using TwoDayDemoBank.Common.EventBus;
using TwoDayDemoBank.Domain.IntegrationEvents;
using TwoDayDemoBank.Domain.Services;

namespace TwoDayDemoBank.Domain.Commands
{
    public record Withdraw : IRequest
    {
        public Withdraw(Guid accountId, Money amount)
        {
            AccountId = accountId;
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }

        public Guid AccountId { get; }
        public Money Amount { get; }
    }


    public class WithdrawHandler : IRequestHandler<Withdraw>
    {
        private readonly IAggregateRepository<Account, Guid> _accountEventsService;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IEventProducer _eventProducer;

        public WithdrawHandler(IAggregateRepository<Account, Guid> accountEventsService, ICurrencyConverter currencyConverter, IEventProducer eventProducer)
        {
            _accountEventsService = accountEventsService;
            _currencyConverter = currencyConverter;
            _eventProducer = eventProducer;
        }

        public async Task Handle(Withdraw command, CancellationToken cancellationToken)
        {
            var account = await _accountEventsService.RehydrateAsync(command.AccountId);
            if (null == account)
                throw new ArgumentOutOfRangeException(nameof(Withdraw.AccountId), "invalid account id");

            account.Withdraw(command.Amount, _currencyConverter);

            await _accountEventsService.PersistAsync(account);

            var @event = new TransactionHappened(Guid.NewGuid(), account.Id);
            await _eventProducer.DispatchAsync(@event, cancellationToken);
        }
    }
}