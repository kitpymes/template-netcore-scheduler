using FluentValidation;

namespace Tests.Application
{
    public class CreateWorkerTestDataValidator : AbstractValidator<CreateWorkerTestCommand>
    {
        public CreateWorkerTestDataValidator()
        {
            RuleFor(x => x.Data)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("No ha indicado ningún valor para {PropertyName}.");
        }
    }
}