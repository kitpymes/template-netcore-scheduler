using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application
{
    public record GetJobsQuery(int Count = 10) : IRequest<IEnumerable<JobDto>>;

    public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, IEnumerable<JobDto>>
    {
        private readonly Persistence.AppDBContext _context;
        private readonly IMapper _mapper;

        public GetJobsQueryHandler(Persistence.AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = _context.Jobs
                .Take(request.Count)
                .OrderBy(x => x.ScheduledAt)
                .AsNoTracking();

            return await _mapper.ProjectTo<JobDto>(jobs).ToListAsync();
        }
    }
}