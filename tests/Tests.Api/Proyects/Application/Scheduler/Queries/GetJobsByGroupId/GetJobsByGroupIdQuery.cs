using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application
{
    public record GetJobsByGroupIdQuery(string GroupId, int? Count = 10) : IRequest<IEnumerable<JobDto>>;

    public class GetJobsByGroupIdQueryHandler : IRequestHandler<GetJobsByGroupIdQuery, IEnumerable<JobDto>>
    {
        private readonly Persistence.AppDBContext _context;
        private readonly IMapper _mapper;

        public GetJobsByGroupIdQueryHandler(Persistence.AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetJobsByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Jobs.Where(job => job.GroupId == request.GroupId);

            if (request.Count.HasValue)
            {
                query = query.Take(request.Count.Value);
            }

            var jobs = query.OrderBy(x => x.ScheduledAt).AsNoTracking();

            return await _mapper.ProjectTo<JobDto>(jobs).ToListAsync();
        }
    }
}