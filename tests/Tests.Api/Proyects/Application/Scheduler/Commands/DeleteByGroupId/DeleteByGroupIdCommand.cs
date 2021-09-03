using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tests.Persistence;

namespace Tests.Application
{
    public record DeleteByGroupIdCommand(string GroupId) : IRequest<bool>;

    public class DeleteWorkerTestCommandHandler : IRequestHandler<DeleteByGroupIdCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;

        public DeleteWorkerTestCommandHandler(
            AppDBContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteByGroupIdCommand command, CancellationToken cancellationToken)
        {
            var jobs = await _context.Jobs.Where(job => job.GroupId == command.GroupId).ToListAsync();

            _context.Jobs.RemoveRange(jobs);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new CreatedJobEvent($"All jobs has been REMOVED at {DateTime.Now.ToLongDateString()}: [GroupId: {command.GroupId}]"), cancellationToken);

            return true;
        }
    }
}