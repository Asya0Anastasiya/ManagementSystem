using DocumentServiceApi.Exceptions;
using System.Text;
using FluentValidation;
using MediatR;

namespace DocumentServiceApi.MediatR
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure =>
                validationFailure.ErrorMessage + "\n")
            .ToList();

            if (errors.Any())
            {
                var stringBuilder = new StringBuilder();

                foreach (var error in errors)
                {
                    stringBuilder.Append(error);
                }

                throw new InternalException(stringBuilder.ToString());
            }

            return await next();
        }
    }
}
