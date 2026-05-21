using System;
using System.Collections.Generic;
using System.Text;

namespace StoreZoneV2API.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "SAR";
        
        private Money() { } // For EF Core

        public Money(decimal amount, string currency = "SAR")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));
            Amount = amount;
            Currency =currency.Trim().ToUpper();
        }
    }
}
