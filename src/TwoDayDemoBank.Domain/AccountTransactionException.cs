using System;

namespace TwoDayDemoBank.Domain
{
    public class AccountTransactionException : Exception
    {
        public Account Account { get; }

        public AccountTransactionException(string s, Account account) : base(s)
        {
            Account = account;
        }
    }
}