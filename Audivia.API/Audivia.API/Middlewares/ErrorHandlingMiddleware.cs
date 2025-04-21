namespace Audivia.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {

        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        // =======================================
        // === Constructor
        // =======================================

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        // =======================================
        // === Methods
        // =======================================

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, ex.Message);
                await WriteToResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (NotSupportedException ex)
            {
                _logger.LogError(ex, ex.Message);
                await WriteToResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogError(ex, ex.Message);
                await WriteToResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await WriteToResponse(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, ex.Message);
                await WriteToResponse(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await WriteToResponse(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
            }
        }

        /// <summary>
        /// Write essential data to response object
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task WriteToResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = statusCode,
                Message = message,
                Success = false
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
