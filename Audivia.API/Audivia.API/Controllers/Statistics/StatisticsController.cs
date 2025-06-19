using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Statistics;
using Audivia.Domain.ModelResponses.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Statistics
{
    [Route("api/v1/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("revenue")]
        public async Task<ActionResult<GetRevenueStatResponse>> GetRevenueStatistics([FromQuery] GetRevenueStatRequest request)
        {
             var result = await _statisticsService.GetRevenueStatistics(request);
             return Ok(result);
        }

        [HttpGet("tours")]
        public async Task<ActionResult<GetTourStatResponse>> GetTourStatistics([FromQuery] GetTourStatRequest request)
        {
            var result = await _statisticsService.GetTourStatistics(request);
            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<ActionResult<GetUserStatResponse>> GetUserStatistics([FromQuery] GetUserStatRequest request)
        {
            var result = await _statisticsService.GetUserStatistics(request);
            return Ok(result);
        }

        [HttpGet("posts")]
        public async Task<ActionResult<GetPostStatResponse>> GetPostStatistics([FromQuery] GetPostStatRequest request)
        {
            var result = await _statisticsService.GetPostStatistics(request);
            return Ok(result);
        }
    }
}
