using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application
{
    public class CreatedJobLoggerEventHandler : INotificationHandler<CreatedJobEvent>
    {
        private readonly ILogger<CreatedJobLoggerEventHandler> _logger;

        public CreatedJobLoggerEventHandler(ILogger<CreatedJobLoggerEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreatedJobEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(notification.message);

            return Task.CompletedTask;
        }
    }
}