using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFR_ReactiveSystems.Frontend.Client.Models
{
    public record Transaction
    {
        public string? CreditorIBAN { get; }
        public string? DebitorIBAN { get; }
        public decimal Amount { get; }
        public DateTime? CreatedAt { get; }
        public int? Id { get; }
    }
}
