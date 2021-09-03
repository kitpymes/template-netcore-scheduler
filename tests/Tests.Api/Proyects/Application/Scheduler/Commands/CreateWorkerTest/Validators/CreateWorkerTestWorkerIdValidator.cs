using FluentValidation;
using System.Linq;
using Tests.Persistence;

namespace Tests.Application
{
    public class CreateWorkerTestWorkerIdValidator : AbstractValidator<CreateWorkerTestCommand>
    {
        private readonly AppDBContext _context;

        public CreateWorkerTestWorkerIdValidator(AppDBContext context)
        {
            _context = context;

            RuleFor(x => x.GroupId)
              .Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("No ha indicado el nombre del {PropertyName}.")
              .Length(2, 50).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.")
              .Must((x, data) => IsDuplicatedWorkerId(x.GroupId, x.WorkerId)).WithMessage("El nombre '{PropertyValue}' del campo '{PropertyName}' ya existe.");
        }

        private bool IsDuplicatedWorkerId(string groupId, string workerId) => !_context.Jobs.Any(x => x.GroupId == groupId && x.WorkerId == workerId);
    }
}