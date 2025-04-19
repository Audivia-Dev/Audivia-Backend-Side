namespace Audivia.Domain.ModelRequests.TransactionHistory
{
    public class CreateTransactionHistoryRequest
    {
        public string? UserId { get; set; }
        public string? TourId { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
    }
}
