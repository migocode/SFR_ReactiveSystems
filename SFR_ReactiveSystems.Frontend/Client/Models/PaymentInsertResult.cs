namespace SFR_ReactiveSystems.Frontend.Client.Models
{
    public record ResponseType
    {
        public Insert_Payments_One Insert_Payments_One { get; set; } = new();
    }

    public record Insert_Payments_One
    {
        public int Id { get; set; }
    }
}


