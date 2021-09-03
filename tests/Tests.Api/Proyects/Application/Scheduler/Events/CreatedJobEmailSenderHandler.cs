using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application
{
    public class CreatedJobEmailSenderHandler : INotificationHandler<CreatedJobEvent>
    {
        public Task Handle(CreatedJobEvent notification, CancellationToken cancellationToken)
        {
            //IMessageSender.Send($"Welcome {notification.FirstName} {notification.LastName} !");
            return Task.CompletedTask;
        }
    }
}