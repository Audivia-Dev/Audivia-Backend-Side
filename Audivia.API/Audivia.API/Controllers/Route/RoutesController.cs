using Audivia.Application.Services.Implemetation;
using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.Answer;
using Audivia.Domain.ModelRequests.Route;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Audivia.API.Controllers.Route
{
    [Route("api/v1/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;
        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            var rs = await _routeService.GetAllRouteAsync();
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateRouteRequest request)
        {
            var rs = await _routeService.CreateRouteAsync(request);
            return Ok(rs);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(string id, [FromBody] UpdateRouteRequest request)
        {
            var rs = await _routeService.UpdateRouteAsync(id, request);
            return Ok(rs);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(string id)
        {
            var rs = await _routeService.DeleteRouteAsync(id);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var rs = await _routeService.GetRouteByIdAsync(id);
            return Ok(rs);
        }
    }
}
