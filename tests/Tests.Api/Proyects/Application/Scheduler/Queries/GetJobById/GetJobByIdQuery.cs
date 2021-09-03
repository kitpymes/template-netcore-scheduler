using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application
{
    public record GetJobByIdQuery(long JobId) : IRequest<JobDto>;

    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDto>
    {
        private readonly Persistence.AppDBContext _context;
        private readonly IMapper _mapper;

        public GetJobByIdQueryHandler(Persistence.AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<JobDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(x => x.Id == request.JobId);

            if (job is null)
            {
                throw new Infrastructure.ErrorException(HttpStatusCode.NotFound, "Customer with given ID is not found.");
            }

            return _mapper.Map<JobDto>(job);
        }
    }
}