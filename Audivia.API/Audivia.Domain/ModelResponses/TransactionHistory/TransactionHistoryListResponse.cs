using Audivia.Commons.Api;
using Audivia.Domain.DTOs;

namespace Audivia.Domain.ModelResponses.TransactionHistory
{
    public class TransactionHistoryListResponse : AbstractApiResponse<List<TransactionHistoryDTO>>
    {
        public override List<TransactionHistoryDTO> Response { get; set; }
    }
}
