using Audivia.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audivia.Application.Services.Implemetation
{
    public class TourReminderBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TourReminderBackgroundService> _logger;
      //  private readonly IReminderService _reminderService;
        public TourReminderBackgroundService(IServiceScopeFactory scopeFactory, ILogger<TourReminderBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var reminderService = scope.ServiceProvider.GetRequiredService<IReminderService>();
                    await reminderService.ProcessTourRemindersAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi khi chạy TourReminderBackgroundService");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                //chạy sau mỗi giờ
            }
        }
    }
    
}
