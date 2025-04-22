using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.TourReview;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.TourReview
{
    [Route("api/v1/tour-reviews")]
    [ApiController]
    public class TourReviewsController : ControllerBase
    {
        private readonly ITourReviewService _tourReviewService;

        public TourReviewsController(ITourReviewService tourReviewService)
        {
            _tourReviewService = tourReviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourReviewRequest request)
        {
            var result = await _tourReviewService.CreateTourReview(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tourReviewService.GetAllTourReviews();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tourReviewService.GetTourReviewById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTourReviewRequest request)
        {
            await _tourReviewService.UpdateTourReview(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _tourReviewService.DeleteTourReview(id);
            return NoContent();
        }
    }
}
