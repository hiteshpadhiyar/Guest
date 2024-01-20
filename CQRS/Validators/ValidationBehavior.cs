using FluentValidation;
using MediatR;
using AutoMapper.Configuration;

namespace Guest.CQRS.Validators
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators.Select(v => v.Validate(context))
                                        .SelectMany(e => e.Errors)
                                        .Where(x => x != null)
                                        .ToList();


            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return next();

        }
    }
}
