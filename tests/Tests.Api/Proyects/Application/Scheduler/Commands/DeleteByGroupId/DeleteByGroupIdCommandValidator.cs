using FluentValidation;
using Tests.Persistence;

namespace Tests.Application
{
    public class DeleteByGroupIdCommandValidator : AbstractValidator<DeleteByGroupIdCommand>
    {
        public DeleteByGroupIdCommandValidator(AppDBContext context)
        {
            Include(new DeleteByGroupIdValidator());
        }
    }
}