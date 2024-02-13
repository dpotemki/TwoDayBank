using System;
using System.Linq;
using FluentAssertions;
using TwoDayDemoBank.Common.Models;
using TwoDayDemoBank.Domain.DomainEvents;
using TwoDayDemoBank.Domain.Services;
using Xunit;

namespace TwoDayDemoBank.Domain.Tests
{
    public class AccountTests
    {

        [Fact]
        public void Create_should_create_valid_Account_instance()
        {
            var currencyConverter = new FakeCurrencyConverter();
            var customer = Customer.Create(Guid.NewGuid(), "lorem", "ipsum", "test@test.com");
            var account = Account.Create(Guid.NewGuid(), customer, Currency.Tenge);
            account.Deposit(new Money(Currency.Tenge, 10), currencyConverter);
            account.Deposit(new Money(Currency.Tenge, 42), currencyConverter);
            account.Withdraw(new Money(Currency.Tenge, 4), currencyConverter);
            account.Deposit(new Money(Currency.Tenge, 71), currencyConverter);

            var instance = BaseAggregateRoot<Account, Guid>.Create(account.Events);
            instance.Should().NotBeNull();
            instance.Id.Should().Be(account.Id);
            instance.OwnerId.Should().Be(customer.Id);
            instance.Balance.Should().NotBeNull();
            instance.Balance.Currency.Should().Be(Currency.Tenge);
            instance.Balance.Value.Should().Be(account.Balance.Value);
        }

        [Fact]
        public void Create_should_return_valid_instance()
        {
            var customer = Customer.Create(Guid.NewGuid(), "lorem", "ipsum", "test@test.com");
            var sut = Account.Create(Guid.NewGuid(), customer, Currency.Tenge);

            sut.Balance.Should().Be(Money.Zero(Currency.Tenge));
            sut.OwnerId.Should().Be(customer.Id);
            sut.Version.Should().Be(1);
        }

        [Fact]
        public void Create_should_add_account_to_customer()
        {
            var customer = Customer.Create(Guid.NewGuid(), "lorem", "ipsum", "test@test.com");

            var account = Account.Create(Guid.NewGuid(), customer, Currency.Tenge);
            customer.Accounts.Should().Contain(account.Id);
        }

        [Fact]
        public void ctor_should_raise_Created_event()
        {
            var customer = Customer.Create(Guid.NewGuid(), "lorem", "ipsum", "test@test.com");

            var accountId = Guid.NewGuid();
            var sut = new Account(accountId, customer, Currency.Tenge);
            
            sut.Events.Count.Should().Be(1);

            var createdEvent = sut.Events.First() as AccountEvents.AccountCreated;
            createdEvent.Should().NotBeNull()
                .And.BeOfType<AccountEvents.AccountCreated>();
            createdEvent.AggregateId.Should().Be(accountId);
            createdEvent.AggregateVersion.Should().Be(0);
            createdEvent.OwnerId.Should().Be(customer.Id);
            createdEvent.Currency.Should().Be(Currency.Tenge);
        }

        [Fact]
        public void Deposit_should_add_value()
        {
            var customer = Customer.Create(Guid.NewGuid(), "lorem", "ipsum", "test@test.com");
            var sut = Account.Create(Guid.NewGuid(), customer, Currency.Tenge);
            var currencyConverter = new FakeCurrencyConverter();

            sut.Balance.Should().Be(Money.Zero(Currency.Tenge));
           
            sut.Deposit(new Money(Currency.Tenge, 1), currencyConverter);
            sut.Balance.Should().Be(new Money(Currency.Tenge, 1));
            sut.Version.Should().Be(2);

            sut.Deposit(new Money(Currency.Tenge, 9), currencyConverter);
            sut.Balance.Should().Be(new Money(Currency.Tenge, 10));
            sut.Version.Should().Be(3);
        }

        [Fact]
        public void Withdraw_should_throw_if_current_balance_is_below_amount()
        {
            var customer = Customer.Create(Guid.NewGuid(), "lorem", "ipsum", "test@test.com");
            var sut = Account.Create(Guid.NewGuid(), customer, Currency.Tenge);
            var currencyConverter = new FakeCurrencyConverter();

            sut.Balance.Should().Be(Money.Zero(Currency.Tenge));
            
            Assert.Throws<AccountTransactionException>(() => sut.Withdraw(new Money(Currency.Tenge, 1), currencyConverter));
        }

        [Fact]
        public void Withdraw_should_remove_value()
        {
            var customer = Customer.Create(Guid.NewGuid(), "lorem", "ipsum", "test@test.com");
            var sut = Account.Create(Guid.NewGuid(), customer, Currency.Tenge);
            var currencyConverter = new FakeCurrencyConverter();

            sut.Deposit(new Money(Currency.Tenge, 10), currencyConverter);

            sut.Withdraw(new Money(Currency.Tenge, 1), currencyConverter);

            sut.Balance.Should().Be(new Money(Currency.Tenge, 9));
            sut.Version.Should().Be(3);
        }
    }
}
