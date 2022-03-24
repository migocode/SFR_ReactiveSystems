using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFR_ReactiveSystems.Frontend.Client.Models
{
    public record Transaction
    {
        public string? CreditorIBAN { get; set; }
        public string? DebitorIBAN { get; set; }
        public decimal Amount { get; set; }
        public string? CreatedAt { get; set; }
        public int? Id { get; set; }
    }
}
