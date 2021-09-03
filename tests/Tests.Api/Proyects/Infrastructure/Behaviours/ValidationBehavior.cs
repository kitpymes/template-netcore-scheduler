using FluentValidation;
using MediatR;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Infrastructure
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : class
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
               .Select(v =>v.Validate(context))
               .SelectMany(result => result.Errors)
               .Where(error => error != null)
               .Select(x => new { x.PropertyName, x.ErrorMessage })
               .Distinct()
               .ToList();

            if (failures.Any())
            {
                throw new ErrorException(HttpStatusCode.BadRequest, failures);
            }

            return await next();
        }
    }
}