using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.TransactionHistory
{
    public class TransactionHistoryResponse : AbstractApiResponse<TransactionHistoryDTO>
    {
        public override TransactionHistoryDTO Response { get; set; }
    }
}
