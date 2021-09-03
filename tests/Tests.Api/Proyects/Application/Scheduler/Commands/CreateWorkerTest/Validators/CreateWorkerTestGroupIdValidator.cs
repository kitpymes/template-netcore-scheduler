using FluentValidation;

namespace Tests.Application
{
    public class CreateWorkerTestGroupIdValidator : AbstractValidator<CreateWorkerTestCommand>
    {
        public CreateWorkerTestGroupIdValidator()
        {
            RuleFor(x => x.GroupId)
              .Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("No ha indicado el nombre del {PropertyName}.")
              .Length(2, 50).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
        }
    }
}