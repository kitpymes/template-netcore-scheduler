using FluentValidation;

namespace Tests.Application
{
    public class GetJobsByGroupIdQueryValidator : AbstractValidator<GetJobsByGroupIdQuery>
    {
        public GetJobsByGroupIdQueryValidator()
        {
            Include(new GetJobsByGroupIdValidator());
        }
    }
}