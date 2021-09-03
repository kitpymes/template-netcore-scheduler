using AutoMapper;
using Kitpymes.Core.Scheduler;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tests.Api.Scheduler;
using Tests.Persistence;

namespace Tests.Application
{
    public record CreateWorkerTestCommand(string GroupId, string WorkerId, WorkerTestData Data) : IRequest<JobDto>;

    public class CreateTestDataJobCommandHandler : IRequestHandler<CreateWorkerTestCommand, JobDto>
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateTestDataJobCommandHandler(
            AppDBContext context,
            IMapper mapper,
            IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<JobDto> Handle(CreateWorkerTestCommand command, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Job>(command);

            await _context.Jobs.AddAsync(model, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new CreatedJobEvent($"New job has been CREATED at {model.ScheduledAt}: [{model.Id} - {model.WorkerId}]"), cancellationToken);

            return _mapper.Map<JobDto>(model);
        }
    }
}