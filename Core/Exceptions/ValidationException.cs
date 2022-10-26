using FluentValidation.Results;

namespace Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Se han producido uno o más errores de validació")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }

        }
    }
}