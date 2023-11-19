using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyTaskPlanner.DataAccess.Data;
using MyTaskPlanner.DataAccess.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskPlanner.DataAccess.Services
{
    public class ReminderService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<ReminderService> _logger;
        private Timer _timer;
        private readonly IHubContext<ReminderHub> _hubContext;
        private readonly IServiceScopeFactory scopeFactory;

        public ReminderService(ILogger<ReminderService> logger, IHubContext<ReminderHub> hubContext, IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            _logger = logger;
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                if (dbContext != null)
                {
                    dbContext.Reminders.ToList().ForEach(item =>
                    {
                        item.Ticket = dbContext.Tasks.FirstOrDefault(x => x.TaskId == item.TaskId);
                    });

                    _hubContext.Clients.All.SendAsync("ReceiveReminder", dbContext.Reminders.
                        Where(p=>p.IsReminderRepeat)?
                        .ToList());
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
