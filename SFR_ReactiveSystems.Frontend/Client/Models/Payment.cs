using System.ComponentModel.DataAnnotations;

namespace SFR_ReactiveSystems.Frontend.Client.Models
{
    public record Payment
    {
        [Required]
        [StringLength(13, ErrorMessage = "IBAN too long (max. 13 characters)", MinimumLength = 10)]
        public string? CreditorIBAN { get; set; }
        public string? DebitorIBAN { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
