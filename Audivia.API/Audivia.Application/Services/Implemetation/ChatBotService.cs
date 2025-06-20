using Audivia.Application.Services.Interface;
using Audivia.Domain.ModelRequests.ChatBot;
using Audivia.Domain.ModelResponses.ChatBot;
using Microsoft.Extensions.Logging;
using Google.Cloud.Dialogflow.Cx.V3;
using Microsoft.Extensions.Configuration;
using Audivia.Domain.Models.ChatBotHistory;
using Audivia.Infrastructure.Repositories.Interface;
using MongoDB.Bson;

namespace Audivia.Application.Services.Implemetation
{
    public class ChatBotService : IChatBotService
    {
        private readonly SessionsClient _sessionsClient;
        private readonly ILogger<ChatBotService> _logger;
        private readonly IChatBotSessionRepository _sessionRepository;
        private readonly IChatBotMessageRepository _messageRepository;

        private readonly string _projectId;
        private readonly string _locationId;
        private readonly string _agentId;
        private const int SessionTimeoutMinutes = 30;

        public ChatBotService(ILogger<ChatBotService> logger, 
                              IConfiguration configuration, 
                              IChatBotSessionRepository sessionRepository,
                              IChatBotMessageRepository messageRepository)
        {
            _logger = logger;
            _sessionRepository = sessionRepository;
            _messageRepository = messageRepository;

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
            if (string.IsNullOrEmpty(request.Text) || string.IsNullOrEmpty(request.ClientSessionId) || string.IsNullOrEmpty(request.UserId) || !ObjectId.TryParse(request.UserId, out _))
            {
                _logger.LogWarning("Text or ClientSessionId or UserId is missing in the request.");
                throw new ArgumentException("Text or SessionId or UserId is missing in the request.");
            }

            ChatBotSession? currentSession = await _sessionRepository.GetByClientSessionIdAsync(request.ClientSessionId);

            if (currentSession != null && currentSession.LastAccessedAt < DateTime.UtcNow.AddMinutes(-SessionTimeoutMinutes))
            {
                _logger.LogInformation($"Session {currentSession.ClientSessionId} timed out. Marking as inactive.");
                currentSession.IsActive = false;
                await _sessionRepository.Update(currentSession);
                currentSession = null; 
            }

            // create new session
            if (currentSession == null)
            {
                _logger.LogInformation($"No active session found or session timed out for ClientSessionId {request.ClientSessionId}. Creating a new session.");
                currentSession = new ChatBotSession
                {
                    ClientSessionId = request.ClientSessionId, 
                    UserId = request.UserId,
                    CreatedAt = DateTime.UtcNow,
                    LastAccessedAt = DateTime.UtcNow,
                    IsActive = true,
                };
                await _sessionRepository.Create(currentSession);
            }
            else
            {
                currentSession.LastAccessedAt = DateTime.UtcNow;
                await _sessionRepository.Update(currentSession);
            }
            
            // create message and send to model
            var userMessage = new ChatBotMessage
            {
                ChatSessionId = currentSession.Id,
                ClientSessionId = currentSession.ClientSessionId,
                Sender = SenderType.User,
                Text = request.Text,
                Timestamp = DateTime.UtcNow
            };

            var sessionName = SessionName.FromProjectLocationAgentSession(_projectId, _locationId, _agentId, currentSession.Id);

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
                // get message from bot
                DetectIntentResponse dialogflowResponse = await _sessionsClient.DetectIntentAsync(detectIntentRequest);

                var replyText = "No response from bot.";
                if (dialogflowResponse.QueryResult?.ResponseMessages?.Count > 0)
                {
                    foreach (var message in dialogflowResponse.QueryResult.ResponseMessages)
                    {
                        if (message.Text?.Text_?.Count > 0)
                        {
                            replyText = message.Text.Text_[0];
                            break;
                        }
                    }
                }
                var replyTime = DateTime.UtcNow;
                var botMessage = new ChatBotMessage
                {
                    ChatSessionId = currentSession.Id,
                    ClientSessionId = currentSession.ClientSessionId, 
                    Sender = SenderType.Bot,
                    Text = replyText,
                    Timestamp = replyTime
                };
                await _messageRepository.Create(userMessage);
                await _messageRepository.Create(botMessage);

                return new MessageResponse { Reply = replyText, Sender = SenderType.Bot, Timestamp = replyTime.ToString() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Dialogflow CX for session {SessionId}.", currentSession.ClientSessionId);
                throw; 
            }
        }

        public async Task<IEnumerable<MessageResponse>> GetChatHistoryAsync(string clientSessionId, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(clientSessionId))
            {
                _logger.LogWarning("ClientSessionId is required to fetch chat history.");
                throw new ArgumentNullException(nameof(clientSessionId));
            }

            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10; 

            _logger.LogInformation($"Fetching chat history for ClientSessionId: {clientSessionId}, Page: {pageNumber}, Size: {pageSize}");
            
            var list = await _messageRepository.GetMessagesByClientSessionIdAsync(clientSessionId, pageNumber, pageSize);
            List<MessageResponse> response = new List<MessageResponse>();
            foreach (ChatBotMessage m in list)
            {
                response.Add(new MessageResponse { Reply =  m.Text, Sender = m.Sender, Timestamp = m.Timestamp.ToString() });
            }
            return response;
        }
    }
}
