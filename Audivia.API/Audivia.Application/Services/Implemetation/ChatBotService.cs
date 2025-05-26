using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatBot;
using Audivia.Domain.ModelResponses.ChatBot;
using Microsoft.Extensions.Logging;
using Google.Cloud.Dialogflow.Cx.V3;
using Microsoft.Extensions.Configuration;

namespace Audivia.Application.Services.Implemetation
{
    public class ChatBotService : IChatBotService
    {
        private readonly SessionsClient _sessionsClient;
        private readonly ILogger<ChatBotService> _logger;

        private readonly string _projectId;
        private readonly string _locationId;
        private readonly string _agentId;

        public ChatBotService(ILogger<ChatBotService> logger, IConfiguration configuration)
        {
            _logger = logger;

            _projectId = configuration["DialogflowSettings:ProjectId"];
            _locationId = configuration["DialogflowSettings:LocationId"];
            _agentId = configuration["DialogflowSettings:AgentId"];

            if (string.IsNullOrEmpty(_projectId) || string.IsNullOrEmpty(_locationId) || string.IsNullOrEmpty(_agentId))
            {
                _logger.LogError("Dialogflow settings (ProjectId, LocationId, AgentId) are not configured correctly in appsettings.json.");
                throw new InvalidOperationException("Dialogflow settings are missing or invalid.");
            }

            _sessionsClient = new SessionsClientBuilder
            {
                Endpoint = $"{_locationId}-dialogflow.googleapis.com"
            }.Build();
        }


        public async Task<MessageResponse> DetectIntentAsync(MessageRequest request)
        {
            if (string.IsNullOrEmpty(request.Text) || string.IsNullOrEmpty(request.SessionId))
            {
                _logger.LogWarning("Text or SessionId is missing in the request.");
                throw new ArgumentException("Text or SessionId is missing in the request.");
            }

            var sessionName = SessionName.FromProjectLocationAgentSession(_projectId, _locationId, _agentId, request.SessionId);

            var queryInput = new QueryInput
            {
                Text = new TextInput { Text = request.Text },
                LanguageCode = "vi" 
            };

            var detectIntentRequest = new DetectIntentRequest
            {
                SessionAsSessionName = sessionName,
                QueryInput = queryInput
            };

            try
            {
                DetectIntentResponse response = await _sessionsClient.DetectIntentAsync(detectIntentRequest);

                var reply = "No response from bot.";
                if (response.QueryResult?.ResponseMessages?.Count > 0)
                {
                    foreach (var message in response.QueryResult.ResponseMessages)
                    {
                        if (message.Text?.Text_?.Count > 0)
                        {
                            reply = message.Text.Text_[0];
                            break;
                        }
                    }
                }

                return new MessageResponse { Reply = reply };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Dialogflow CX for session {SessionId}.", request.SessionId);
                throw;
            }
        }
    }
}
