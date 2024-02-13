﻿using System.ComponentModel.DataAnnotations;

namespace TwoDayDemoBank.Service.Core.DTOs
{
    public class WithdrawDto
    {
        [Required]
        public string CurrencyCode { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}