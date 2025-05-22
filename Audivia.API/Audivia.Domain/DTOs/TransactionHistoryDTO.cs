using Audivia.Domain.Models;

namespace Audivia.Domain.DTOs
{
    public class TransactionHistoryDTO
    {
        public string Id { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public string? AudioCharacterId { get; set; } = null;
        public double Amount { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Tour Tour { get; set; }
    }
}
