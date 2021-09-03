using AutoMapper;
using Kitpymes.Core.Scheduler;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application
{
    public record GetNextJobsQuery(int Count = 10) : IRequest<IEnumerable<JobDto>>;

    public class GetNextJobsQueryHandler : IRequestHandler<GetNextJobsQuery, IEnumerable<JobDto>>
    {
        private readonly Persistence.AppDBContext _context;
        private readonly IMapper _mapper;

        public GetNextJobsQueryHandler(Persistence.AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetNextJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = _context.Jobs
                .Where(job => job.State == JobState.Scheduled.ToString() && job.RetriesCounter < job.MaxRetries)
                .Take(request.Count)
                .OrderBy(x => x.ScheduledAt)
                .AsNoTracking();

            return await _mapper.ProjectTo<JobDto>(jobs).ToListAsync();
        }
    }
}