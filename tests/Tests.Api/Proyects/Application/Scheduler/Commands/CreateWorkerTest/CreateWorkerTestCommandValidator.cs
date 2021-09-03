using FluentValidation;
using Tests.Persistence;

namespace Tests.Application
{
    public class CreateWorkerTestCommandValidator : AbstractValidator<CreateWorkerTestCommand>
    {
        public CreateWorkerTestCommandValidator(AppDBContext context)
        {
            Include(new CreateWorkerTestGroupIdValidator());

            Include(new CreateWorkerTestWorkerIdValidator(context));

            Include(new CreateWorkerTestDataValidator());
        }
    }
}