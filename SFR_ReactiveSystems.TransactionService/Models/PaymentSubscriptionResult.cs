using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFR_ReactiveSystems.TransactionService.Models
{
    public record PaymentSubscriptionResult
    {
        public Payment Payments { get; set; } = new Payment();
    }
}
