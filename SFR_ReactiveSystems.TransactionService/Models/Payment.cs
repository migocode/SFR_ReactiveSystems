using System.ComponentModel.DataAnnotations;

namespace SFR_ReactiveSystems.TransactionService.Models
{
    public record Payment
    {
        public string? CreditorIBAN { get; set; }
        public string? DebitorIBAN { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    }
}
