﻿using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.TransactionHistory;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Transaction
{
    [Route("api/v1/transaction-histories")]
    [ApiController]
    public class TransactionHistoriesController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoriesController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionHistoryRequest request)
        {
            var result = await _transactionHistoryService.CreateTransactionHistory(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transactionHistoryService.GetAllTransactionHistorys();
            return Ok(result);
        }
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllTransactionByAdmin()
        {
            var result = await _transactionHistoryService.GetAllTransactionsWithUserAndTour();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _transactionHistoryService.GetTransactionHistoryById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTransactionHistoryRequest request)
        {
            await _transactionHistoryService.UpdateTransactionHistory(id, request);
            return NoContent();
        }
        [HttpPut("character/{id}")]
        public async Task<IActionResult> UpdateAudioCharacter(string id, [FromBody] UpdateCharacterIdRequest request)
        {
            await _transactionHistoryService.UpdateCharacterSelection(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _transactionHistoryService.DeleteTransactionHistory(id);
            return NoContent();
        }

        [HttpGet("transaction/{userId}")]
        public async Task<IActionResult> GetHistoryTransactionByUser(string userId)
        {
            var rs = await _transactionHistoryService.GetTransactionHistoryByUserId(userId);
            return Ok(rs);
        }
        [HttpGet("user/{userId}/tour/{tourId}")]
        public async Task<IActionResult> GetTransactionHistoryByUserIdAndTourId(string userId, string tourId)
        {
            var rs = await _transactionHistoryService.GetTransactionHistoryByUserIdAndTourId(userId, tourId);
            return Ok(rs);
        }
    }
}
