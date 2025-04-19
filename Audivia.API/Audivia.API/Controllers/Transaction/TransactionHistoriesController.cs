using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.TransactionHistory;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Transaction
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionHistoriesController : ControllerBase
    {
        private readonly ITransactionHistoryService _audioTourService;

        public TransactionHistoriesController(ITransactionHistoryService audioTourService)
        {
            _audioTourService = audioTourService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionHistoryRequest request)
        {
            var result = await _audioTourService.CreateTransactionHistory(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _audioTourService.GetAllTransactionHistorys();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _audioTourService.GetTransactionHistoryById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTransactionHistoryRequest request)
        {
            await _audioTourService.UpdateTransactionHistory(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _audioTourService.DeleteTransactionHistory(id);
            return NoContent();
        }
    }
}
